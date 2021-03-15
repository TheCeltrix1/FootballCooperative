using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightChange : MonoBehaviour
{
    public Image dark;
 
    // colour variables
    public bool changecolour = false;


    float starttime;

    void Start()
    {
        dark = GetComponent<Image>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (changecolour)
        {
            dark.enabled = true;
        }

        
        
    }

}
