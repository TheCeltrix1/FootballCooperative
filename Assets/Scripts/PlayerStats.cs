using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Slider healthSlider;
   // public Image fill;

    public Slider staminaSlider;
   // public Image stamFill;
    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
       // healthSlider.value = health;
    }
    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }

    public void SetMaxStamina(int stamina)
    {
        staminaSlider.maxValue = stamina;
        //staminaSlider.value = stamina;  
    }
    public void SetStamina(int stamina)
    {
        staminaSlider.value = stamina;

    }
}
