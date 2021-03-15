using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    public Light mylight;
    //Range Variables
    public bool changeRange = false;
    public float rangeSpeed = 1.0f;
    public float maxRange = 10.0f;
    //Intensity Variables
    public bool ChangeIntenity = false;
    public float intensitychangespeed = 1.0f;
    public float maxintenisty = 10.0f;
    // colour variables
    public bool changecolours = false;
    public float colourchangespeed = 1.0f;
    public Color startcolour;
    public Color Endcolour;

    float starttime;

    void Start()
    {
        mylight = GetComponent<Light>();
        starttime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeRange)
        {
            mylight.range = Mathf.PingPong(Time.time * rangeSpeed, maxRange);
        }
        if (ChangeIntenity)
        {
            mylight.intensity = Mathf.PingPong(Time.time * intensitychangespeed, maxintenisty);
        }
        if (changecolours)
        {
            float t = (Mathf.Sin(Time.time - starttime * colourchangespeed));
            mylight.color = Color.Lerp(startcolour, Endcolour, t);
        }
    }

}
