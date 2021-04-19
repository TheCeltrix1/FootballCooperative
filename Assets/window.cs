using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class window : MonoBehaviour
{
    private GameManager manager;
    private Image windowimage;
    private float currentstamina;

    public Image sunshine;
    public float raystrength; // 0 is completely blank while 1 is completely full so if you want 
    public float threshold;
   // public float threshold2;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        windowimage = GetComponent<Image>();
        currentstamina = GameManager.currentMaxStamina;
        Debug.Log(currentstamina);
        if (currentstamina >= threshold)
        {
            
            Color currcolor = sunshine.color;
            currcolor.a = raystrength;
            sunshine.color = currcolor;
            windowimage.sprite = Resources.Load<Sprite>("window_open");
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
