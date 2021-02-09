using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameover = false;
    public float delay = 2f;
    public GameObject completelvlUI;
    public GameObject tryagain;

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

    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
