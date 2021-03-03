using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class souvenirs : MonoBehaviour
{
    public float threshold;
    private GameManager manager;
    private SpriteRenderer spirte;
    // Start is called before the first frame update
    void Start()
    {
     manager  =  FindObjectOfType<GameManager>();
        this.spirte = GetComponent<SpriteRenderer>();
        if (threshold <= manager.trips)
        {
            this.spirte.enabled = true;
            Debug.Log("ta");
        }
    }
    void check()
    {
   
    }

}
