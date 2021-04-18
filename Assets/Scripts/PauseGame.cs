using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseGame : MonoBehaviour
{
   
    public static bool paused = true;
    public bool changePaused = true;
    private static bool _tutorialPlayed = false;
    public float textCount;
    public TextMeshProUGUI characterText;
    public TextMeshProUGUI choresText;
    public TextMeshProUGUI coOpText;
    // Start is called before the first frame update
    void Start()
    {
        if (!_tutorialPlayed)
        {
            characterText.enabled = true;
            choresText.enabled = false;
            coOpText.enabled = false;
            paused = true;
            _tutorialPlayed = true;

        }
        else
        {
            paused = false;
            characterText.enabled = false;
            choresText.enabled = false;
            coOpText.enabled = false;
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

            if( textCount == 2)
            {
               if (Input.GetMouseButtonDown(0))
               {
                    characterText.enabled = false;
                    choresText.enabled = true;
                    textCount--;
               }
            }


           else if (textCount == 1)
           {
                if (Input.GetMouseButtonDown(0))
                {
                    choresText.enabled = false;
                    coOpText.enabled = true;
                    textCount --;
                }
           }

            else if(textCount == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ResumeGameTime();
                    coOpText.enabled = false;
                    paused = false;
                }
            }
            

        }
            
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
