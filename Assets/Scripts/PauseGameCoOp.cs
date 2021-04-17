using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseGameCoOp : MonoBehaviour
{
    public bool paused = true;
    public bool changePaused = true;
    private static bool _tutorialPlayed = false;
    public float textCount;
    public TextMeshProUGUI ObsticalsText;
    public TextMeshProUGUI howToPlayText;
    // Start is called before the first frame update
    void Start()
    {
        if (!_tutorialPlayed)
        {
            howToPlayText.enabled = true;
            ObsticalsText.enabled = false;
            paused = true;
            _tutorialPlayed = true;

        }
        else
        {
            paused = false;
            ObsticalsText.enabled = false;
            howToPlayText.enabled = false;
        }


        textCount = 1f;
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

            if (textCount == 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    howToPlayText.enabled = false;
                    ObsticalsText.enabled = true;
                    textCount--;
                }
            }

            else if (textCount == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ResumeGameTime();
                    ObsticalsText.enabled = false;
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
