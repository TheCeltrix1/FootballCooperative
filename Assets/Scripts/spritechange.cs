using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritechange : MonoBehaviour
{
    // public SpriteRenderer render;
    //  public Sprite completed;
    // Start is called before the first frame update
    private Animator ani;
    private SpriteRenderer cone;
    private AudioSource _sfx;
   
    void Start()
    {
        //  render = GetComponent<SpriteRenderer>();
        _sfx = GetComponent<AudioSource>();
        ani = GetComponentInChildren<Animator>();
        cone = GetComponent<SpriteRenderer>();
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
            cone.enabled = false;
            if (!_sfx.isPlaying) _sfx.Play();
            //Debug.Log("wrong");
        }
    }
   
}
