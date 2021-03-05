using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2D : MonoBehaviour
{
    public float speed = 3.0f;
    public float jump = 20.0f;
    public LayerMask JumpLayer;
    public player2D play;
    public float endPos;
    public float startPos;

    //Second Player Character code
    public bool jumping = false;
    public Vector2 backgroundBallPosition = new Vector2(3,3);
    public Vector2 foregroundBallPosition = new Vector2(3, 3);
    public GameObject currentBallPosition;

    private Vector2 _targetPosition;
    private Collider2D _playerCollider;
    private Rigidbody2D _playerRigidbody;
    private bool _endAnimation = false;

    public string breathingAnimationName;
    private Animator _playerAnimator;
    private float animationDelaytime;
    private RuntimeAnimatorController _animatorRTC;

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

        _endAnimation = false;
        startPos = transform.position.x;
        _playerRigidbody = GetComponent<Rigidbody2D>();
        endPos = transform.position.x + speed * _totalMaxStamina;
        _playerCollider = GetComponent<Collider2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerAnimator.SetBool("noStamina", false);
    }

    void Update()
    {
        #region Stamina Management
        if (stamina <= 0 && _currentMaxStamina < _totalMaxStamina && !_endAnimation)
        {
            if (_playerCollider.IsTouchingLayers(JumpLayer))
            {
                _endAnimation = true;
                //LERP SPEED TO A STOP
                _playerAnimator.SetBool("noStamina", true);
                _playerAnimator.SetBool("maxStamina", true);

                _animatorRTC = _playerAnimator.runtimeAnimatorController;
                for (int i = 0; i < _animatorRTC.animationClips.Length; i++)
                {
                    if (_animatorRTC.animationClips[i].name == breathingAnimationName) animationDelaytime = _animatorRTC.animationClips[i].length;
                }
                FindObjectOfType<GameManager>().gohome(animationDelaytime);
            }
            return;
        }
        else if (stamina <= 0 && _currentMaxStamina >= _totalMaxStamina && !_endAnimation)
        {
            _endAnimation = true;
            //Insert Kick animation and Ienumerator to return home.
            _playerAnimator.SetBool("noStamina", true);
            _playerAnimator.SetBool("maxStamina", true);
            Debug.Log("endgame");
            return;
        }
        #endregion
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        _playerRigidbody.velocity = new Vector2(speed,_playerRigidbody.velocity.y);

        //Animation
        _playerAnimator.SetFloat("speed", speed);
        _playerAnimator.SetBool("isGrounded", _playerCollider.IsTouchingLayers(JumpLayer));

        #region Movement
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
        #endregion

        #region Kick to background
        //Second Player Character code and passing the ball
        if (!_playerCollider.IsTouchingLayers(JumpLayer))
        {
            _targetPosition = backgroundBallPosition;
            Debug.Log("Ball to Background");
        }
        else if (_playerCollider.IsTouchingLayers(JumpLayer))
        {
            _targetPosition = foregroundBallPosition;
            Debug.Log("Ball to Foreground");
        }

        currentBallPosition.transform.position = Vector2.MoveTowards(currentBallPosition.transform.position, new Vector2(transform.position.x, transform.position.y) + _targetPosition, 15 * Time.deltaTime );
        #endregion
    }
    void Jump()
    {
        if (!_playerCollider.IsTouchingLayers(JumpLayer))
        {
            return;
        }
        Vector2 jumpVelocityToAdd = new Vector2(0f, jump);
        _playerRigidbody.velocity += jumpVelocityToAdd;

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
