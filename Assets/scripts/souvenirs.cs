using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    
public class souvenirs : MonoBehaviour
{
    public float threshold;
    private GameManager manager;
    private Image spirte;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        this.spirte = GetComponent<Image>();
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
