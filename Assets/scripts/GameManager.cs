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
        GameObject[] objs = GameObject.FindGameObjectsWithTag("manager");
        if(objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
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
        Invoke("mainmenu", delay);
    
            
    }

    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void mainmenu()
    {
        maxenergy += 2;
        maxhealth += 1;
        SceneManager.LoadScene("blah");
    }
}
