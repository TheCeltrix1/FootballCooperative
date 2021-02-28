using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Slider healthSlider;
    public GameManager manager;
    public Slider staminaSlider;

    #region statistics
    private float _stamina;
    private float _totalMaxStamina;
    private float _currentMaxStamina;

    private float _health;
    private float _totalMaxHealth;
    private float _currentMaxHealth;
    #endregion

    void Start()
    {
        _stamina = GameManager.stamina;
        _totalMaxStamina = GameManager.maxenergy;
        _currentMaxStamina = GameManager.currentMaxStamina;

        _health = GameManager.health;
        _totalMaxHealth = GameManager.maxhealth;
        _currentMaxHealth = GameManager.currentMaxHealth;

        staminaSlider.value = GameManager.maxenergy;
        SetMaxHealth(_totalMaxHealth);
        SetMaxStamina(_totalMaxStamina);
        SetCurrentHealth(_currentMaxHealth);
        SetCurrentStamina(_currentMaxStamina);
    }

    #region Stat Settings
    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
    }
    public void SetMaxStamina(float stamina)
    {
        staminaSlider.maxValue = stamina;
    }

    public void SetCurrentHealth(float health)
    {
        healthSlider.value = health;
    }

    public void SetCurrentStamina(float stamina)
    {
        staminaSlider.value = stamina;
    }
    #endregion

    void Update()
    {
        healthSlider.value = GameManager.health;
        staminaSlider.value = GameManager.stamina;
    }

}
