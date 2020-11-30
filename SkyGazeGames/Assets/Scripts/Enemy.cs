using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Variables

    //PUBLIC VARIABLES
    //The max health of the enemy
    public int maxHealth;
    //blood particle
    public GameObject BloodParticle;

    //PRIVATE VARIABLES
    //The current health of the enemy
    private int currentHealth;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Set the health to the max health
        currentHealth = maxHealth;
    }

    //Method for taking damage
    public void TakeDamage(int damage)
    {
        Instantiate(BloodParticle, transform.position, Quaternion.identity);
        //subtract
        currentHealth -= damage;
        //debug to show that it is working
        Debug.Log(currentHealth);
        //If health less than 0
        if(currentHealth <= 0)
        {
            //Die
            Die();
        }
    }

    //Method for die
    public void Die()
    {
        Instantiate(BloodParticle, transform.position, Quaternion.identity);
        //Debug log to show it is working
        Debug.Log(Time.time + this.gameObject.name + "/Enemy Died/");
        //Destroy the gameobject
        Destroy(gameObject);
    }
}
