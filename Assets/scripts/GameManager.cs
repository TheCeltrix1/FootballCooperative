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
    public float maxenergy;
    public float maxhealth;
  

  public void endlevel()
    {
        completelvlUI.SetActive(true);
    }

    private void Awake()
    {
     
        DontDestroyOnLoad(this.gameObject);
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

    public void gohome()
    {
        Invoke("mainmenu", 0.1f);
    
            
    }
    public void endgame()
    {
        completelvlUI.SetActive(true);
    }
    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CancelInvoke("restart");
    }
    void mainmenu()
    {
        Debug.Log("loading");
        maxenergy += 2;
        maxhealth += 1;
        SceneManager.LoadScene("blah");
        CancelInvoke("mainmenu");
    }
}
