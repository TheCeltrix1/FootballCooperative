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
    public Text Text;
    public bool loaded = false;
    public static Player_walking move;
    // Start is called before the first frame update
    void Start()
    {
        fadIn.canvasRenderer.SetAlpha(0f);
        fadInWeel.canvasRenderer.SetAlpha(0f);
        Text.canvasRenderer.SetAlpha(0f);
        fadInStatic = fadIn;
        move = FindObjectOfType<Player_walking>();
    }

    void Update()
    {
        if (Input.anyKey && loaded)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void LoadingScreem(Transform trans)
    {
        if (Vector3.Distance(trans.position,GameManager.instance.playerWalking.gameObject.transform.position) <= 30) 
        {
            fadIn.CrossFadeAlpha(1, 2, false);
            fadInWeel.CrossFadeAlpha(1, 2, false);
            Text.CrossFadeAlpha(1, 2, false);
            loaded = true;
        }
    }

    public static IEnumerator LoadTransition(float duration, GameObject obj = default(GameObject))
    {
        fadInStatic.CrossFadeAlpha(1, duration / 2, false);
        yield return new WaitForSeconds(duration / 2);
        if (obj != null) obj.SetActive(false);
        move.movementspeed = 300;
        fadInStatic.CrossFadeAlpha(0, duration / 2, false);
        yield return null;
    }
}
