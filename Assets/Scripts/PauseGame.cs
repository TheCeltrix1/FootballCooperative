using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseGame : MonoBehaviour
{

    public static bool paused = true;
    public bool changePaused = true;
    public static bool tutorialPlayed = false;
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
        if (!tutorialPlayed)
        {
            characterText.enabled = true;
            characterText2.enabled = false;
            characterText3.enabled = false;
            choresText.enabled = false;
            coOpText.enabled = false;
            paused = true;
            tutorialPlayed = true;
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

        if (paused && Input.GetMouseButtonDown(0))
        {
            characterText.enabled = false;
            characterText2.enabled = false;
            characterText3.enabled = false;
            choresText.enabled = false;
            coOpText.enabled = false;

            switch (textCount)
            {
                case 4:
                    characterText2.enabled = true;
                    break;

                case 3:
                    characterText3.enabled = true;
                    break;

                case 2:
                    choresText.enabled = true;
                    break;

                case 1:
                    coOpText.enabled = true;
                    break;

                case 0:
                    ResumeGameTime();
                    paused = false;
                    break;
            }

            textCount--;
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
