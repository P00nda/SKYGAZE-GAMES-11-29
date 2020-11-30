using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //The name of the unit
    public string unitName;
    //What it says when we encounter it
    public string encountermessage;
    //What is says when it regens
    public string regenMessage;

    //The damage it does and how much it heals
    public int damage;
    public int healAmount;

    //The health
    public int maxHealth;
    [HideInInspector]
    public int currentHealth;

    //At the start of the game..
    private void Start()
    {
        //...set the health of the unit to the maxhealth
        currentHealth = maxHealth;
    }

    //Taking damage
    public bool TakeDamage(int _damage)
    {
        //Decrease the health
        this.currentHealth -= _damage;

        //Check for dead
        if (currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Healing
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            //Make sure that we don't go over the max health
            currentHealth = maxHealth;
        }
        
    }
}
