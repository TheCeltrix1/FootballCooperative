using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritechange : MonoBehaviour
{
    // public SpriteRenderer render;
    //  public Sprite completed;
    // Start is called before the first frame update
    private Animator ani;
   
    void Start()
    {
        //  render = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ani.SetBool("start", true);
           
        }
    }
   
}
