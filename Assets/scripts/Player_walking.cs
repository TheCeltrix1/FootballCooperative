using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_walking : MonoBehaviour
{
    public float movementspeed = 1;
    public bool facingRight = true;

    [SerializeField] private Animator _playerAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        if (GetComponent<Animator>()) _playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerAnimator != null)
        {
            var movement = Input.GetAxis("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementspeed;

            //Animation
            _playerAnimator.SetFloat("speed", Mathf.Abs(movement));

            if (Input.GetKeyDown(KeyCode.A))
            {
                Flip();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Flip();
            }
        }
    }


    void Flip()
    {
        if (facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            Debug.Log("left");
        }
        else if (!facingRight)
        {
            transform.Rotate(0f, 0f, 0f);
            Debug.Log("right");
        }
        facingRight = !facingRight;
    }
}
