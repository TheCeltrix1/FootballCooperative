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
    public static bool running = false;

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
    #endregion

    private void Awake()
    {
        Scene sce = SceneManager.GetActiveScene();
        if (instance != null && instance != this) Destroy(gameObject);
        else if(sce.name != "Menu")
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.sceneLoaded += Test;
    }

    private void Test(Scene scene, LoadSceneMode mode)
    {
        if (FindObjectOfType<player2D>())
        {
            playerScript = FindObjectOfType<player2D>();
            running = true;
        }
        else if (FindObjectOfType<Player_walking>())
        {
            playerWalking = FindObjectOfType<Player_walking>();
            running = false;
        }
        else
        {
            running = false;
            PlayerStats TomHasBigPP = instance.GetComponent<PlayerStats>();
            if (TomHasBigPP) TomHasBigPP.enabled = false;
        }
        if (FindObjectOfType<PlayerStats>())
        {
            _playerSliderStats = FindObjectOfType<PlayerStats>();
            if (running) _playerSliderStats.progressBarMax = playerScript.endPos - playerScript.transform.position.x;
        }
        stamina = currentMaxStamina;
        health = currentMaxHealth;
    }

    private void Update()
    {
        if (running)
        {
            stamina -= Time.deltaTime;
            playerScript.stamina = stamina;
            _playerSliderStats.SetCurrentHealth(health);
            _playerSliderStats.SetCurrentStamina(stamina);
            _playerSliderStats.progressBar = playerScript.gameObject.transform.position.x - playerScript.startPos;
        }
        else
        {
            stamina = currentMaxStamina;
            health = currentMaxHealth;
        }
    }

    public void death(float deathDelay)
    {
        Debug.Log("death");
        if (gameover == false)
        {
            //   gameover = true;
            //  tryagain.SetActive(true);
            Invoke("restart", deathDelay);
        }
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
        Debug.Log("loading");
        currentMaxStamina += 2;
        currentMaxHealth += 1;
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
        //   completelvlUI.SetActive(true);
        Invoke("ending", time);
    }

    public void endlevel()
    {
        completelvlUI.SetActive(true);
    }
}
