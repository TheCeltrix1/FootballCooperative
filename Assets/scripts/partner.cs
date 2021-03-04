using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partner : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.Translate(Vector2.right * speed * Time.deltaTime);


    }
}
