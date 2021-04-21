using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadInLoading : MonoBehaviour
{
    public Image fadIn;
    public static Image fadInStatic;
    public Image[] fadInWeel;
    public Text[] Text;
    private int _currentScene;
    private int _loadingScene;
    private bool _loaded = false;
    //public static Player_walking move;
    public AudioSource doorOpenSFX;
    // Start is called before the first frame update
    void Start()
    {
        fadIn.canvasRenderer.SetAlpha(0f);
        int i = 0;
        foreach  (Image canvieBoi in fadInWeel)
        {
            fadInWeel[i].canvasRenderer.SetAlpha(0f);
            i++;
        }
        i = 0;
        foreach (Text item in Text)
        {
            Text[i].canvasRenderer.SetAlpha(0f);
            i++;
        }
        fadInStatic = fadIn;
        //move = FindObjectOfType<Player_walking>();
        _currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (Input.anyKey && _loaded)
        {
            SceneManager.LoadScene(_loadingScene);
        }
    }

    public void SceneToLoad(int num)
    {
        _loadingScene = num;
    }

    public IEnumerator LoadingScreem(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        fadIn.CrossFadeAlpha(1, 2, false);
        int i = 0;
        foreach (Image canvieBoi in fadInWeel)
        {
            fadInWeel[i].CrossFadeAlpha(1, 2, false);
            i++;
        }
        i = 0;
        foreach (Text item in Text)
        {
            Text[i].CrossFadeAlpha(1, 2, false);
            i++;
        }
        _loaded = true;
        if (doorOpenSFX) 
        { 
            doorOpenSFX.Play(); 
        }
        yield return null;
    }

    public static IEnumerator LoadTransition(float duration, GameObject obj = default(GameObject))
    {
        fadInStatic.CrossFadeAlpha(1, duration / 2, false);
        yield return new WaitForSeconds(duration / 2);
        if (obj != null) obj.SetActive(false);
        //move.movementspeed = 300;
        fadInStatic.CrossFadeAlpha(0, duration / 2, false);
        yield return null;
    }

    public void CheckDistance(Transform trans)
    {
        if (Vector3.Distance(trans.position, GameManager.instance.playerWalking.gameObject.transform.position) <= 30) StartCoroutine("LoadingScreem",0);
    }
}
