using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadInLoading : MonoBehaviour
{
    public Image fadIn;
    public Image fadInWeel;
    public bool loaded =  false;
    // Start is called before the first frame update
    void Start()
    {
        fadIn.canvasRenderer.SetAlpha(0f);
        fadInWeel.canvasRenderer.SetAlpha(0f);
    }

     void Update()
    {
       if (Input.anyKey && loaded)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void LoadingScreem()
    {
        fadIn.CrossFadeAlpha(1, 2, false);
        fadInWeel.CrossFadeAlpha(1, 2, false);
        loaded = true;
    }
}
