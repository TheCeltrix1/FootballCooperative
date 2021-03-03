using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public Slider healthSlider;
    public Slider staminaSlider;

    private GameManager manager;
    private Slider _progressBar;
    private GameObject _objectHolder;
    private bool _progressBarAvailable;

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
        _objectHolder = GameObject.FindGameObjectWithTag("ObjectHolder");
        if (_objectHolder != null) 
        {
            staminaSlider = _objectHolder.GetComponent<ObjectHolder>().staminaSliderObject;
            healthSlider = _objectHolder.GetComponent<ObjectHolder>().healthSliderObject;
            _progressBar = _objectHolder.GetComponent<ObjectHolder>().progressSliderObject;
        }
        _progressBarAvailable = (_progressBar) ? true : false;
        if (_progressBarAvailable) _progressBar.maxValue = GameManager.maxenergy;

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
        if (_progressBarAvailable) _progressBar.value = _currentMaxStamina - GameManager.stamina;
    }
}
