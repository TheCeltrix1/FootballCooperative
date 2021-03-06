﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseGameCoOp : MonoBehaviour
{
    [Header("Tutorial")]
    public bool paused = true;
    public bool changePaused = true;
    public static bool tutorialPlayed = false;
    public float textCount;
    public TextMeshProUGUI ObsticalsText;
    public TextMeshProUGUI howToPlayText;
    public TextMeshProUGUI StaminaText;


    [Header("Countdown")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI readyText;
    public float timeRemaining = 3;
    public bool timerIsRunning = false;
    public TextMeshProUGUI goText;
    // Start is called before the first frame update
    void Start()
    {
        if (!tutorialPlayed)
        {
          
            howToPlayText.enabled = true;
            ObsticalsText.enabled = false;
            StaminaText.enabled = false;
            goText.enabled = false;
            timeText.enabled = false;
            readyText.enabled = false;
            paused = true;
            tutorialPlayed = true;

        }
        else
        {
       
            paused = false;
            ObsticalsText.enabled = false;
            howToPlayText.enabled = false;
            StaminaText.enabled = false;
            timeText.enabled = false;
            goText.enabled = false;
            readyText.enabled = false;
            player2D.k_canPlay = true;
        }

        
        textCount = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (paused && changePaused)
        {
            PauseGameTime();
            changePaused = false;
        }

        if (paused)
        {

            if (textCount == 2)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    howToPlayText.enabled = false;
                    StaminaText.enabled = true;
                    textCount--;
                }
            }

           else if (textCount == 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StaminaText.enabled = false;
                    ObsticalsText.enabled = true;
                    textCount--;
                }
            }

            else if (textCount == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    
                    ObsticalsText.enabled = false;
                    //timerIsRunning = true;
                    StartCoroutine(CountDown());
                    timeText.enabled = true;
                    readyText.enabled = true;
                    paused = false;
                }
            }


        }

    }

    IEnumerator CountDown()
    {
        while(timeRemaining > 0)
        {
            timeText.text = timeRemaining.ToString();

            yield return new WaitForSecondsRealtime(1);

            timeRemaining--;
        }
   
        readyText.enabled = false;
        timeText.enabled = false;
        player2D.k_canPlay = true;
        ResumeGameTime();
        GameManager.instance.running = true;
        goText.enabled = true;
        yield return new WaitForSecondsRealtime(1);
        goText.enabled = false;
        yield return null;
    }

    public void PauseGameTime()
    {
        Time.timeScale = 0;
    }

    public void ResumeGameTime()
    {
        Time.timeScale = 1;
    }
}
