using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject panel;
    //public Image fadIn;
    //public Image fadInWheel;

    public void Start()
    {
        //fadIn.canvasRenderer.SetAlpha(0f);
        //fadInWheel.canvasRenderer.SetAlpha(0f);
    }

    /*public void PlayGamelevel()
    {
        fadIn.CrossFadeAlpha(1, 2, false);
        fadInWheel.CrossFadeAlpha(1, 2, false);
        StartCoroutine("Fade");

    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MarcusScene");
    }*/


    public void GameMenuControls()
    {
        //SceneManager.LoadScene("Controls");
        panel.SetActive(true);
    
    }

    public void ReturnGameMenu()
    {
        //SceneManager.LoadScene("Controls");
        panel.SetActive(false);

    }
}
