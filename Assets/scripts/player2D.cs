using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2D : MonoBehaviour
{
    public float speed = 3.0f;
    public float jump = 20.0f;
    public LayerMask JumpLayer;
    public float endPos;
    public float startPos;
    public bool canPlay = true;

    //Second Player Character code
    public bool jumping = false;
    public GameObject currentBallPosition;
    public Vector3 backgroundBallPosition = new Vector3(3, 3, 0);
    public Vector3 foregroundBallPosition = new Vector3(3, 3, 0);

    public GameObject backgroundPlayer;
    private float _backgroundPlayerY;
    private float _backgroundPlayerX;

    private float _ballTransitionSpeed = 0.25f;
    private float _ballTransitionStage;
    private float _ballForegroundScale = 2f;
    private float _ballBackgroundScale = ((float)(2f/3f) * 2f);
    private float _ballYPosition;

    private Vector2 _targetPosition;
    private Collider2D _playerCollider;
    private Rigidbody2D _playerRigidbody;
    private bool _endAnimation = false;

    public string breathingAnimationName;
    public string kickAnimationName;
    public string fallAnimationName;
    private float _playerSlowdownTime = .5f;
    private float _playerSlowdownTimer;
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
        endPos = transform.position.x + speed * _totalMaxStamina;

        _playerSlowdownTimer = _playerSlowdownTime;
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<Collider2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerAnimator.SetBool("noStamina", false);
        _ballYPosition = currentBallPosition.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        _backgroundPlayerY = backgroundPlayer.GetComponent<SpriteRenderer>().bounds.size.y / 1.5f;
        _backgroundPlayerX = backgroundBallPosition.x - (backgroundPlayer.GetComponent<SpriteRenderer>().bounds.size.x/3);
    }

    void Update()
    {
        if (canPlay) {
            #region Stamina Management
            if (stamina <= 0 && _currentMaxStamina < _totalMaxStamina && !_endAnimation)
            {
                if (_playerCollider.IsTouchingLayers(JumpLayer))
                {
                    _endAnimation = true;
                    StartCoroutine("StopMoving");
                    _playerAnimator.SetBool("noStamina", true);
                    _playerAnimator.SetBool("maxStamina", true);
                    animationDelaytime = AnimatorNextClipLength(breathingAnimationName);
                    GameManager.instance.gohome(animationDelaytime);
                }
                return;
            }
            else if (stamina <= 0 && _currentMaxStamina >= _totalMaxStamina && !_endAnimation)
            {
                if (_playerCollider.IsTouchingLayers(JumpLayer))
                {
                    _endAnimation = true;
                    StartCoroutine("StopMoving");
                    _playerAnimator.SetBool("noStamina", true);
                    _playerAnimator.SetBool("maxStamina", true);
                    animationDelaytime = AnimatorNextClipLength(kickAnimationName);

                    //GAME COMPLETE SHENANIGANS
                    GameManager.instance.endgame(animationDelaytime);
                    Debug.Log("endgame");
                }
                return;
            }
            #endregion
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            if (!_endAnimation) _playerRigidbody.velocity = new Vector2(speed, _playerRigidbody.velocity.y);

            //Animation
            _playerAnimator.SetFloat("speed", speed);
            _playerAnimator.SetBool("isGrounded", _playerCollider.IsTouchingLayers(JumpLayer));
            #region Movement
            if (Input.GetMouseButtonDown(0))
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
        }
        backgroundPlayer.transform.position = new Vector2(transform.position.x + _backgroundPlayerX, _backgroundPlayerY);

        #region Kick to background
        StartCoroutine("PassBall");

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

    #region Animator Clip Info Finder

    public float AnimatorNextClipLength(string name)
    {
        _animatorRTC = _playerAnimator.runtimeAnimatorController;
        for (int i = 0; i < _animatorRTC.animationClips.Length; i++)
        {
            if (_animatorRTC.animationClips[i].name == name) return _animatorRTC.animationClips[i].length;
        }
        return 1f;
    }

    #endregion

    #region Coroutines
    private IEnumerator StopMoving()
    {
        _playerRigidbody.velocity = new Vector2(Mathf.Lerp(speed,0, 1 - (_playerSlowdownTimer / _playerSlowdownTime)), _playerRigidbody.velocity.y);
        _playerSlowdownTimer -= Time.deltaTime;
        if (_playerRigidbody.velocity.magnitude == 0) StopCoroutine("StopMoving");
        yield return null;
    }

    private IEnumerator PassBall()
    {
        if (!_playerCollider.IsTouchingLayers(JumpLayer))
        {
            _targetPosition = backgroundBallPosition;
            _ballTransitionStage += Time.deltaTime;
        }
        else
        {
            _targetPosition = foregroundBallPosition;
            _ballTransitionStage -= Time.deltaTime;
        }
        _ballTransitionStage = Mathf.Clamp(_ballTransitionStage,0,_ballTransitionSpeed);
        currentBallPosition.transform.position = new Vector2(transform.position.x, _ballYPosition) + Vector2.Lerp(foregroundBallPosition, backgroundBallPosition, _ballTransitionStage / _ballTransitionSpeed);
        float scale = Mathf.Lerp(_ballForegroundScale, _ballBackgroundScale, _ballTransitionStage / _ballTransitionSpeed);
        currentBallPosition.transform.localScale = new Vector3(scale,scale,scale);
        yield return null;
    }
    #endregion

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "block")
        {
            Debug.Log("hit");
            canPlay = false;

            animationDelaytime = AnimatorNextClipLength(fallAnimationName);
            GameManager.instance.death(animationDelaytime * 2);
        }
    }
}
