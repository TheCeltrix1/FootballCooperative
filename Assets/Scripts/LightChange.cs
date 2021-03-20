using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightChange : MonoBehaviour
{
    private Image dark;
    public Color colour;
    public Color colour2;
    
    public float threshold;
    public float thresshold2;
    public float thresshold3;
    private GameManager manager;


    void Start()
    {
        dark = GetComponent<Image>();
        manager = GameManager.instance;
  
     
    }

    // Update is called once per frame
    void Update()
    {
      //  if (changecolour)
      //  {
      //      dark.enabled = true;
     //       dark.color = colour2;
      //  }

        if (threshold <= manager.trips)
        {
            dark.enabled = true;
            dark.color = colour;
       
        }
        if(thresshold2 <= manager.trips)
        {
            dark.enabled = true;
            dark.color = colour2;
        }

    }

}
