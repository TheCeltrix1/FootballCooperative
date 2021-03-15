using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightChange : MonoBehaviour
{
  
    public Light2D mylight;
    //Range Variables

    //Intensity Variables
    public bool ChangeIntenity = false;
    public float intensityspeed = 1.0f;
    public float maxintenisty = 10.0f;
    // colour variables
    public bool changecolours = false;
    public float colourchangespeed = 1.0f;
    public Color startcolour;
    public Color Endcolour;

    float starttime;

    void Start()
    {
        mylight = GetComponent<Light2D>();
        starttime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (ChangeIntenity)
        {
            mylight.intensity = Mathf.PingPong(Time.time * intensityspeed, maxintenisty);
        }
        if (changecolours)
        {
            float t = (Mathf.Sin(Time.time - starttime * colourchangespeed));
            mylight.color = Color.Lerp(startcolour, Endcolour, t);
        }
    }

}
