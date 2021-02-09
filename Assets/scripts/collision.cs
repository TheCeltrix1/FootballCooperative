using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    public player play;
   void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "block")
        {
            play.enabled = false;
            FindObjectOfType<GameManager>().death();
        }
    }

}
