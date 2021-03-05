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
        if (instance != null) Destroy(gameObject);
        else
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
            if (running) _playerSliderStats.progressBarMax = _playerScript.endPos - _playerScript.transform.position.x;
        }
        stamina = currentMaxStamina;
        health = currentMaxHealth;
    }

    private void Update()
    {
        if (running)
        {
            stamina -= Time.deltaTime;
            _playerScript.stamina = stamina;
            _playerSliderStats.SetCurrentHealth(health);
            _playerSliderStats.SetCurrentStamina(stamina);
            _playerSliderStats.progressBar = _playerScript.gameObject.transform.position.x - _playerScript.startPos;
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
        SceneManager.LoadScene("blah");
        CancelInvoke("mainmenu");
    }

    //I don't know what these do or if they are even used.
    public void endgame()
    {
        completelvlUI.SetActive(true);
    }

    public void endlevel()
    {
        completelvlUI.SetActive(true);
    }
}
