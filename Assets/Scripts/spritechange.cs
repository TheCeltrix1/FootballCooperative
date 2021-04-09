using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritechange : MonoBehaviour
{
    public SpriteRenderer render;
    public Sprite completed;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
         //   Color tmp = render.color;
           // tmp.a = 0f;
            render.sprite = completed;
         //   render.color = tmp;
           // render.color += 1;
           // commented out code is me trying to make the aplha go to zero and incrase to make a fade in effect
        }
    }
   
}
