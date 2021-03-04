using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_walking : MonoBehaviour
{
    public float movementspeed = 1;
    public bool facingRight = true;

    [SerializeField] private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementspeed;

        //Animation
        playerAnimator.SetFloat("speed", Mathf.Abs(movement));

        if (Input.GetKeyDown(KeyCode.A))
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Flip();
        }
    }


    void Flip()
    {
        if(facingRight == true)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
            Debug.Log("left");
            return;
        }
        if (!facingRight == true)
        {
            facingRight = true;
            transform.Rotate(0f, 0f, 0f);
            Debug.Log("right");
        }
    }
}
