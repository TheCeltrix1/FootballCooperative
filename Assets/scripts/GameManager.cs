using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    bool gameover = false;
    public float delay = 2f;
    public float trips;
    public GameObject completelvlUI; // Is this used?
    public GameObject tryagain; // Is this used?
    public bool running = false;

    public float staminaIncrease = 1;
    public float healthIncrease = .75f;
    private player2D playerScript;
    private PlayerStats _playerSliderStats;
    public Player_walking playerWalking;

    #region Stat Variables
    public static float stamina;
    public static float maxenergy = 10;
    public static float currentMaxStamina = 5;

    public static float health;
    public static float maxhealth = 10;
    public static float currentMaxHealth = 5;

    //Reset Vars
    private static float _staminaReset = 5;
    private static float _healthReset = 5;
    #endregion

    private void Awake()
    {
        Scene sce = SceneManager.GetActiveScene();
        if (instance != null && instance != this) Destroy(gameObject);
        else if (sce.name != "Menu")
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.sceneLoaded += Test;
    }

    private void Test(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)Destroy(instance.gameObject);
        Cursor.visible = true;
        if (FindObjectOfType<player2D>())
        {
            playerScript = FindObjectOfType<player2D>();
            if(PauseGameCoOp.tutorialPlayed) running = true;
            Cursor.visible = false;
        }
        else if (FindObjectOfType<Player_walking>())
        {
            playerWalking = FindObjectOfType<Player_walking>();
            running = false;
        }
        else
        {
            running = false;
            PlayerStats tomHasBigPP = instance.GetComponent<PlayerStats>();
            if (tomHasBigPP) tomHasBigPP.enabled = false;
        }
        if (FindObjectOfType<PlayerStats>())
        {
            _playerSliderStats = FindObjectOfType<PlayerStats>();
            if (running || (!PauseGameCoOp.tutorialPlayed && FindObjectOfType<player2D>())) _playerSliderStats.progressBarMax = playerScript.endPos - playerScript.transform.position.x;
        }
        stamina = currentMaxStamina;
        health = currentMaxHealth;
    }

    private void Update()
    {
        stamina = Mathf.Clamp(stamina, 0, maxenergy);
        if (running)
        {
            stamina -= Time.deltaTime;
            playerScript.stamina = stamina;
            _playerSliderStats.SetCurrentHealth(health);
            _playerSliderStats.SetCurrentStamina(stamina);
            _playerSliderStats.progressBar = playerScript.gameObject.transform.position.x - playerScript.startPos;
        }
        else if (FindObjectOfType<Player_walking>())
        {
            stamina = currentMaxStamina;
            health = currentMaxHealth;
        }
    }

    public void death(float deathDelay)
    {
        if (gameover == false)
        {
             // gameover = true;
            //  tryagain.SetActive(true);
            Invoke("restart", deathDelay);
        }
    }

    #region MiscFunctions
    public void ReturnHome()
    {
        currentMaxStamina += staminaIncrease;
        currentMaxHealth += healthIncrease;
        trips += 1;
    }

    public void gohome(float time)
    {
        Invoke("mainmenu", time);
    }

    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CancelInvoke("restart");
    }

    void mainmenu()
    {
        //Debug.Log("loading");
        currentMaxStamina += staminaIncrease;
        currentMaxHealth += healthIncrease;
        trips += 1;
        SceneManager.LoadScene(1);
        CancelInvoke("mainmenu");
    }

    void ending()
    {
        SceneManager.LoadScene(3);
        running = false;
        CancelInvoke("ending");
    }

    //I don't know what these do or if they are even used.
    public void endgame(float time)
    {
        Invoke("ending", time);
    }

    public void endlevel()
    {
        completelvlUI.SetActive(true);
    }

    public static void ResetManager()
    {
        PauseGame.tutorialPlayed = false;
        PauseGameCoOp.tutorialPlayed = false;
        currentMaxStamina = _staminaReset;
        currentMaxHealth = _healthReset;
    }
    #endregion
}
