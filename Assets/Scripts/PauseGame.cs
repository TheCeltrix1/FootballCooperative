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
    public TextMeshProUGUI characterText2;
    public TextMeshProUGUI characterText3;
    public TextMeshProUGUI choresText;
    public TextMeshProUGUI coOpText;
    public GameObject darkness;
    // Start is called before the first frame update
    void Start()
    {
        if (!_tutorialPlayed)
        {
            characterText.enabled = true;
            characterText2.enabled = false;
            characterText3.enabled = false;
            choresText.enabled = false;
            coOpText.enabled = false;
            paused = true;
            _tutorialPlayed = true;
            darkness.SetActive(true);
        }
        else
        {
            paused = false;
            characterText.enabled = false;
            characterText2.enabled = false;
            characterText3.enabled = false;
            choresText.enabled = false;
            coOpText.enabled = false;
            darkness.SetActive(false);
        }


        textCount = 4f;
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
            if (textCount == 4)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    characterText.enabled = false;
                    characterText2.enabled = true;
                    textCount--;
                }
            }

            else if (textCount == 3)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    characterText2.enabled = false;
                    characterText3.enabled = true;
                    textCount--;
                }
            }

            else  if ( textCount == 2)
            {
               if (Input.GetMouseButtonDown(0))
               {
                    characterText3.enabled = false;
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
        darkness.SetActive(false);
    }
}
