using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadInLoading : MonoBehaviour
{
    public Image fadIn;
    public static Image fadInStatic;
    public Image fadInWeel;
    public bool loaded = false;
    // Start is called before the first frame update
    void Start()
    {
        fadIn.canvasRenderer.SetAlpha(0f);
        fadInWeel.canvasRenderer.SetAlpha(0f);
        fadInStatic = fadIn;
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

    public static IEnumerator LoadTransition(float duration, GameObject obj = default(GameObject))
    {
        fadInStatic.CrossFadeAlpha(1, duration / 2, false);
        yield return new WaitForSeconds(duration / 2);
        if (obj != null) obj.SetActive(false);
        fadInStatic.CrossFadeAlpha(0, duration / 2, false);
        yield return null;
    }
}
