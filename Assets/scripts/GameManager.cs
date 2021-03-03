﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameover = false;
    public float delay = 2f;
    public float trips;
    public GameObject completelvlUI;
    public GameObject tryagain;
    public static bool running = false;
    private player2D _playerScript;
    private PlayerStats _playerSliderStats;

    #region Stat Variables
    public static float stamina;
    public static float maxenergy = 10;
    public static float currentMaxStamina = 5;

    public static float health;
    public static float maxhealth = 10;
    public static float currentMaxHealth = 5;
    #endregion

    private void Awake()
    {
        if (FindObjectOfType<player2D>()) 
        {
            _playerScript = FindObjectOfType<player2D>();
            running = true;
        }
        else if (FindObjectOfType<Player_walking>())
        {
            running = false;
        }
        if (FindObjectOfType<PlayerStats>())
        {
            _playerSliderStats = FindObjectOfType<PlayerStats>();
        }
        stamina = currentMaxStamina;
        health = currentMaxHealth;
     //   DontDestroyOnLoad(this.gameObject);
        
    }

    private void Update()
    {
        if (running) {
            stamina -= Time.deltaTime;
            _playerScript.stamina = stamina;
            _playerSliderStats.SetCurrentHealth(health);
            _playerSliderStats.SetCurrentStamina(stamina);
        }
    }

    public void endlevel()
    {
        completelvlUI.SetActive(true);
    }

    public void death()
    {
        Debug.Log("death");
        if (gameover == false)
        {
            //   gameover = true;
            //  tryagain.SetActive(true);
            Invoke("restart", delay);
        }
    }

    public void gohome()
    {
        Invoke("mainmenu", 0.1f);
    }

    public void endgame()
    {
        completelvlUI.SetActive(true);
    }

    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CancelInvoke("restart");
    }

    void mainmenu()
    {
        Debug.Log("loading");
        currentMaxStamina += 2;
        currentMaxHealth += 1;
        trips += 1;
        SceneManager.LoadScene("blah");
        CancelInvoke("mainmenu");
    }
}
