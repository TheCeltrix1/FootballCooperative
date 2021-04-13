using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditSceneTextMovement : MonoBehaviour
{
    public float timer = 10;
    public float speed = 5f;
    public Image fadIn;
    public bool stopTimer = false;
    // Start is called before the first frame update
    void Start()
    {
        fadIn.canvasRenderer.SetAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);

        timer -= Time.deltaTime;

        if(timer <= 0 && !stopTimer)
        {
            StartCoroutine("Fade");
            stopTimer = true;
        }
    }

    IEnumerator Fade()
    {
        fadIn.CrossFadeAlpha(1, 5, false);
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(0);
    }


}


