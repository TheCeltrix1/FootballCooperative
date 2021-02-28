using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class interact : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isInrange;
    public KeyCode interactkey;
    public UnityEvent interactAction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInrange)
        {
            if (Input.GetKeyDown(interactkey))
            {
                interactAction.Invoke();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInrange = true;
            Debug.Log("player is in range");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInrange = false;
            Debug.Log("player is out of range");
        }
    }
    public void hi()
        {
        Debug.Log("hi");
        SceneManager.LoadScene("SampleScene");
        }
}
