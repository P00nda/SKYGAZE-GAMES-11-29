using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This enum will represent what state the game is in
public enum GameState
{
    //The start of the game
    START,
    //The player's turn
    PLAYERTURN,
    //The enemy's turn
    ENEMYTURN,
    //Turn going on to prevent multiple attacks
    TURN,
    //If the player wins
    WON,
    //If the player loses
    LOST
}

public class BattleSystem : MonoBehaviour
{
    #region variables
    //The reference for the player that we will instantiate
    public GameObject player;
    //The reference for the enemy that we will instantiate
    public GameObject enemy;

    //Where the player spawns
    public Transform playerSpawnPoint;
    //Where the enemy spawns
    public Transform enemySpawnPoint;
    //A reference to the state
    public GameState state;

    //References for the player UI
    public BattleHUD playerHUD;
    //References for the enemy UI
    public BattleHUD enemyHUD;

    //References for the Unit script
    Unit playerUnit;
    Unit enemyUnit;

    //Dialogue Text reference
    Text dialogueText;

    //AUDIO TIME!! :D

    //The audio source
    AudioSource srce;

    //the sound when you get hurt
    public AudioClip hurtClip;

    //The sound when you attack
    public AudioClip attackClip;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //At the start, we determine that the game is at the START state
        state = GameState.START;

        //and get the audio source
        srce = GetComponent<AudioSource>();
        //Get reference for dialogue text
        dialogueText = GameObject.Find("Canvas/DialogueAndControl/DialogueText").GetComponent<Text>();
        StartCoroutine(SetUpBattle());
    }

    //Method for setting up battle
    IEnumerator SetUpBattle()
    {
        //Spawn the player
        GameObject playerInst = Instantiate(player, playerSpawnPoint);
        //Get reference for unit script
        playerUnit = playerInst.GetComponent<Unit>();
        //Spawn enemy
        GameObject enemyInst = Instantiate(enemy, enemySpawnPoint);
        //Get reference for unit script
        enemyUnit = enemyInst.GetComponent<Unit>();

        //Check for null
        if(dialogueText != null)
        {
            //Display message
            dialogueText.text = enemyUnit.encountermessage;
        }

        //Set the HUD ui
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        //Wait 2 secs
        yield return new WaitForSeconds(2f);
        
        //Call playerTurn
        PlayerTurn();
    }

    //Player's Turn
    public void PlayerTurn()
    {
        //Change the gamestate
        state = GameState.PLAYERTURN;
        //Show that it is the player's turn
        dialogueText.text = "Your turn!";
    }

    //For the buttons
    public void OnAttackButton()
    {
        //make sure the state is correct
        if (state != GameState.PLAYERTURN) return;

        //Start the attack function
        StartCoroutine(Attack());
    }

    //for the heal button
    public void OnHealButton()
    {
        //Check that it is the player's turn
        if (state != GameState.PLAYERTURN) return;

        //Heal
        StartCoroutine(Heal());
    }

    //IEnum for healing
    IEnumerator Heal()
    {
        //Make sure we don't attack/heal twice
        state = GameState.TURN;

        //Use heal method in Unit script
        playerUnit.Heal(playerUnit.healAmount);

        //Update the UI
        playerHUD.UpdateHealthBar(playerUnit.currentHealth);

        //Update the dialogue text
        dialogueText.text = playerUnit.regenMessage;

        //Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        //Update the state
        state = GameState.ENEMYTURN;
        //Enemy's turn!
        StartCoroutine(EnemyTurn());
    }

    //IEnum for attacking
    IEnumerator Attack()
    {
        //Play the sound!
        srce.PlayOneShot(attackClip);

        //Make sure we don't attack twice
        state = GameState.TURN;

        //Check if the enemy is deaed
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        //Update the ui
        enemyHUD.UpdateHealthBar(enemyUnit.currentHealth);
        //Update dialogue text
        dialogueText.text = ($"You attacked {enemyUnit.unitName}!");

        //Wait for 2 secs
        yield return new WaitForSeconds(2f);

        //Check if the enemy is dead
        if (isDead)
        {
            //If it is, you won
            state = GameState.WON;
            EndBattle();
        }
        else
        {
            //If not, continue on to enemy's turn
            state = GameState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    //Ending the battle
    public void EndBattle()
    {
        //If we won, update dialogue text
        if (state == GameState.WON) {
            dialogueText.text = "You won!";
            Destroy(enemyUnit.gameObject);
        }
        else if(state == GameState.LOST)
        {
            //If we didn't
            dialogueText.text = "You lost.";
            Destroy(playerUnit.gameObject);
        }

    }

    //IEnum for enemy's turn
    IEnumerator EnemyTurn()
    {
        //Update the dialogue text
        dialogueText.text = $"{enemyUnit.unitName} is attacking...";

        //Wait for 2 secs
        yield return new WaitForSeconds(2f);

        //Check if player is dead (and damage the player)
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        //play sound
        srce.PlayOneShot(hurtClip);
        
        //Update dialogue ui
        dialogueText.text = $"{enemyUnit.unitName} attacked you and did {enemyUnit.damage} damage!";

        //Update ui
        playerHUD.UpdateHealthBar(playerUnit.currentHealth);

        //Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        //Check if we are dead or not
        if (isDead)
        {
            state = GameState.LOST;
            EndBattle();
        }
        else
        {
            state = GameState.PLAYERTURN;
            PlayerTurn();
        }
    }
}
