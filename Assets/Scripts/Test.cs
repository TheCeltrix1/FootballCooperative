using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int maxStamina = 100;
    public int currentStamina;
    public PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 0;
        playerStats.SetMaxHealth(maxHealth);

        currentStamina = 0;
        playerStats.SetMaxStamina(maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddHealth(2);
            AddStamina(2);
        }
    }

    void AddHealth(int health)
    {
        Debug.Log("Getting Health");
        currentHealth += health;
      //  playerStats.SetHealth(currentHealth);
    }
    void AddStamina(int stamina)
    {
        Debug.Log("Getting Health");
        currentStamina += stamina;
        playerStats.SetStamina(currentStamina);
    }
}
