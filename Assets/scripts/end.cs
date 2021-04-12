using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class end : MonoBehaviour
{
    public Image fadIn;
    // Start is called before the first frame update
    void Start()
    {
        fadIn.canvasRenderer.SetAlpha(0f);
    }
    public void quit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    public void Credits()
    {
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        fadIn.CrossFadeAlpha(1, 4, false);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(5);
    }
}
