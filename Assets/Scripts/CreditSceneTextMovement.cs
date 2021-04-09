using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditSceneTextMovement : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }
}
