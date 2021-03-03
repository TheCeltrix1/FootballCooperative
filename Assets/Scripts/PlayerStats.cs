using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public Slider healthSlider;
    private GameManager manager;
    public Slider staminaSlider;
    private GameObject objectHolder;

    #region statistics
    private float _stamina;
    private float _totalMaxStamina;
    private float _currentMaxStamina;

    private float _health;
    private float _totalMaxHealth;
    private float _currentMaxHealth;
    #endregion

    private void Awake()
    {
        SceneManager.sceneLoaded += Test;
    }

    void Test(Scene scene, LoadSceneMode mode)
    {
        objectHolder = GameObject.FindGameObjectWithTag("ObjectHolder");
        if (objectHolder != null) 
        {
            staminaSlider = objectHolder.GetComponent<ObjectHolder>().staminaSliderObject;
            healthSlider = objectHolder.GetComponent<ObjectHolder>().healthSliderObject;
        }

        manager = GameManager.instance;
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
