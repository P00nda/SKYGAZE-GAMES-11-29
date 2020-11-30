using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    //The text that displays the Unit's name
    public Text nameText;

    //The slider that represents the health bar
    public Slider healthBar;

    //The text that shows how much health the unit has
    public Text healthText;

    //Setting the hud based on a unit (ON START WE CALL THIS)
    public void SetHUD(Unit unit)
    {
        if (nameText != null)
        {
            nameText.text = unit.unitName;
        }
        healthBar.maxValue = unit.maxHealth;
        healthBar.value = unit.maxHealth;
        healthText.text = ("Health: " + healthBar.value.ToString("F0"));
    }

    //Updating the health bar slider to show the health
    public void UpdateHealthBar(int health)
    {
        healthBar.value = health;
        healthText.text = ("Health: " + healthBar.value.ToString("F0"));
    }
}
