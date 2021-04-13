using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGoal : MonoBehaviour
{
    public float speedScore = 10;
    public Transform goal;
    public bool hasKicked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasKicked)
        {
            transform.position = Vector2.MoveTowards(transform.position, goal.position, speedScore * Time.deltaTime);
        }
    }
}
