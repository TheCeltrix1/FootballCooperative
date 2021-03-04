using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2D : MonoBehaviour
{
    public float speed = 3.0f;
    public float jump = 20.0f;
    public LayerMask JumpLayer;
    public Rigidbody2D playerRigidbody;
    public Collider2D playerCollider;
    public player2D play;

    //Second Player Character code
    public bool jumping = false;
    public Vector2 backgroundBallPosition = new Vector2(3,3);
    public Vector2 foregroundBallPosition = new Vector2(3, 3);
    public GameObject currentBallPosition;
    private Vector2 _targetPosition; 


    [SerializeField] private Animator playerAnimator;

    #region statistics
    public float stamina;
    private float _totalMaxStamina;
    private float _currentMaxStamina;
    #endregion

    void Awake()
    {
        stamina = GameManager.currentMaxStamina;
        _totalMaxStamina = GameManager.maxenergy;
        _currentMaxStamina = GameManager.currentMaxStamina;



        
    }

    void Update()
    {
        if (stamina <= 0 && _currentMaxStamina < _totalMaxStamina)
        {
            FindObjectOfType<GameManager>().gohome();
            Debug.Log("goinghome");
            return;
        }
        if (stamina <= 0 && _currentMaxStamina >= _totalMaxStamina)
        {
            // FindObjectOfType<GameManager>().gohome();
            Debug.Log("endgame");
            return;
        }
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        //Animation
        playerAnimator.SetFloat("speed", speed);
        playerAnimator.SetBool("isGrounded", playerCollider.IsTouchingLayers(JumpLayer));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            //GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jump);
            jumping = true;
        }
        else
        {
            jumping = false;
        }

        //Second Player Character code and passing the ball
        if (!playerCollider.IsTouchingLayers(JumpLayer) && jumping)
        {
            _targetPosition = backgroundBallPosition;
        }
        else if (playerCollider.IsTouchingLayers(JumpLayer) && !jumping)
        {
            _targetPosition = foregroundBallPosition;
        }

        currentBallPosition.transform.position = Vector2.MoveTowards(currentBallPosition.transform.position, new Vector2(transform.position.x, transform.position.y) + _targetPosition, 15 * Time.deltaTime );


    }
    void Jump()
    {
        if (!playerCollider.IsTouchingLayers(JumpLayer))
        {
            return;
        }
        Vector2 jumpVelocityToAdd = new Vector2(0f, jump);
        playerRigidbody.velocity += jumpVelocityToAdd;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "block")
        {
            Debug.Log("hit");
            play.enabled = false;
            FindObjectOfType<GameManager>().death();
        }
    }

}
