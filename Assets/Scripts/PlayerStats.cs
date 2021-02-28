using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Slider healthSlider;
    public GameManager manager;
   // public Image fill;

    public Slider staminaSlider;
    // public Image stamFill;
     void Start()
    {
        manager = FindObjectOfType<GameManager>();
        staminaSlider.value = FindObjectOfType<GameManager>().maxenergy;
        SetHealth(); 
    }
    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
       // healthSlider.value = health;
    }
 //   public void SetHealth(int health)
          public void SetHealth()
    {
      //  healthSlider.value = health;
        healthSlider.value = manager.maxhealth;
    }

    public void SetMaxStamina(int stamina)
    {
        staminaSlider.maxValue = stamina;
        //staminaSlider.value = stamina;  
    }
    public void SetStamina(int stamina)
    {
        staminaSlider.value = stamina;
      //  staminaSlider.value = FindObjectOfType<player2D>().energy;

    }
     void Update()
    {
        healthSlider.value = FindObjectOfType<GameManager>().maxhealth;
        staminaSlider.value = FindObjectOfType<player2D>().energy;
    }
 
}
