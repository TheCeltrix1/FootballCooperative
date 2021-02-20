using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision1 : MonoBehaviour
{
    public Sprite overcomeimage;
    public SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            rend.sprite = overcomeimage;
        }
    }
}
