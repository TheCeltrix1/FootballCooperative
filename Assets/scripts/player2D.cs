using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2D : MonoBehaviour
{
    #region Variables
    [Header("Misc Vars")]
    public float speed = 3.0f;
    public float jump = 20.0f;
    public LayerMask JumpLayer;
    public float endPos;
    public float startPos;
    public static bool canPlay = false;

    [Header("Goal Effects")]
   //cheer and particle effect
    public float cheerdelay;
    public ParticleSystem goaleffect;

    //Second Player Character code
    [Header("BG Player")]
    public bool jumping = false;
    public GameObject currentBallPosition;
    public Vector3 backgroundBallPosition = new Vector3(3, 3, 0);
    public Vector3 foregroundBallPosition = new Vector3(3, 3, 0);

    public GameObject backgroundPlayer;
    private float _backgroundPlayerY;
    private float _backgroundPlayerX;
    private float _backgroundPlayerFinalX;
    private bool _kickSFXBool;

    public GameObject faceBallHit;
    private float _ballTransitionSpeed = 0.25f;
    private float _ballTransitionStage;
    private float _ballForegroundScale = 2f;
    private float _ballBackgroundScale = ((float)(2f / 3f) * 2f);
    private float _ballYPosition;
    public bool ballMove = true;
    private float _speedScore = 80f;
    public Transform goal;

    private Vector2 _targetPosition;
    private Collider2D _playerCollider;
    private Rigidbody2D _playerRigidbody;
    private bool _endAnimation = false;

    [Header("Animations")]
    public string breathingAnimationName;
    public string kickAnimationName;
    public string hitInFaceAnimationName;
    public string fallAnimationName;
    public float animationDelayTimeMultiplyer = 3;
    public Animator goalieAnimator;
    private bool _endAnimationRandomBool = false;
    private float _playerSlowdownTime = 0.5f;
    private float _playerSlowdownTimer;
    private float _animationDelaytime;
    private Animator _playerAnimator;
    private RuntimeAnimatorController _animatorRTC;

    //Audio Vars
    [Header("AudioSFX sources")]
    public AudioSource jumpSFX;
    public AudioSource kickSFX;
    public AudioSource landSFX;
    public AudioSource facePlantSFX;
    public AudioSource finalKickSFX;
    public AudioSource deepBreathsSFX;
    public AudioSource faceHitSFX;
    public AudioSource runningSFX;
    public AudioSource cheerSFX;
    #region Statistics
    [Header("Statistics")]
    public float stamina;
    private float _totalMaxStamina;
    private float _currentMaxStamina;
    #endregion

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
        _backgroundPlayerY = (backgroundPlayer.GetComponent<SpriteRenderer>().bounds.size.y / 1.5f) + (backgroundBallPosition.y / 2);
        _backgroundPlayerX = backgroundBallPosition.x - (backgroundPlayer.GetComponent<SpriteRenderer>().bounds.size.x / 3);
        _backgroundPlayerFinalX = transform.position.x + _backgroundPlayerX;
    }

    void Update()
    {
        if (canPlay)
        {
            #region Stamina Management
            if (stamina <= 0 && _currentMaxStamina < _totalMaxStamina && !_endAnimation)
            {
                if (_playerCollider.IsTouchingLayers(JumpLayer))
                {
                    float loadTime;
                    _endAnimation = true;
                    deepBreathsSFX.Play();
                    _endAnimationRandomBool = (Random.Range(0, 2) == 1);
                    _playerAnimator.SetBool("randomEndAnimation", _endAnimationRandomBool);
                    _playerAnimator.SetBool("noStamina", true);
                    _playerAnimator.SetBool("maxStamina", false);
                    if (_endAnimationRandomBool)
                    {
                        _animationDelaytime = AnimatorNextClipLength(breathingAnimationName);
                        if (!deepBreathsSFX.isPlaying) deepBreathsSFX.Play();
                        deepBreathsSFX.loop = true;
                        loadTime = 3;
                    }
                    else
                    {
                        _animationDelaytime = AnimatorNextClipLength(hitInFaceAnimationName);
                        if (!faceHitSFX.isPlaying) faceHitSFX.Play();
                        faceBallHit.GetComponent<BallHitPlayer>().playerFacePosition = transform;
                        faceBallHit.GetComponent<BallHitPlayer>().StartCoroutine("HitPlayerInTheFace");
                        loadTime = 0.5f;
                    }
                    GameManager.instance.ReturnHome();
                    FindObjectOfType<FadInLoading>().SceneToLoad(1);
                    FindObjectOfType<FadInLoading>().StartCoroutine(FindObjectOfType<FadInLoading>().LoadingScreem(loadTime, 1));
                    canPlay = false;
                }
                return;
            }
            else if (stamina <= 0 && _currentMaxStamina >= _totalMaxStamina && !_endAnimation)
            {
                if (_playerCollider.IsTouchingLayers(JumpLayer))
                {
                    _endAnimation = true;
                    finalKickSFX.Play();
                    _playerAnimator.SetBool("noStamina", true);
                    _playerAnimator.SetBool("maxStamina", true);
                    _animationDelaytime = AnimatorNextClipLength(kickAnimationName);
                    goalieAnimator.SetTrigger("Jump");

                    ballMove = false;
                    cheerSFX.PlayDelayed(cheerdelay);
                    //   Instantiate(goaleffect, currentBallPosition.gameObject.transform);
                    plav();
                    GameManager.instance.endgame(5f);
                    Debug.Log("endgame");
                    canPlay = false;
                }
                return;
            }
            #endregion
            if (stamina <= 0) runningSFX.Stop();

            transform.localRotation = Quaternion.Euler(0, 0, 0);

            #region GameEnd
            if (!_endAnimation)
            {
                _playerRigidbody.velocity = new Vector2(speed, _playerRigidbody.velocity.y);
            }
            else
            {
                backgroundPlayer.GetComponent<Animator>().SetBool("end", true);
                StartCoroutine("StopMoving");
            }
            #endregion

            //Animation
            _playerAnimator.SetFloat("speed", speed);
            _playerAnimator.SetBool("isGrounded", _playerCollider.IsTouchingLayers(JumpLayer));

            #region Movement
            if (Input.GetMouseButtonDown(0) && !_endAnimation)
            {
                Jump();
                jumping = true;
            }
            else jumping = false;
            #endregion
            backgroundPlayer.transform.position = new Vector2(transform.position.x + _backgroundPlayerX, _backgroundPlayerY);
        }
        else backgroundPlayer.transform.position = new Vector2(_backgroundPlayerFinalX, _backgroundPlayerY);

        #region Kick to background
        if (ballMove)
        {
            StartCoroutine("PassBall");
        }
        else currentBallPosition.transform.position = Vector2.MoveTowards(currentBallPosition.transform.position, goal.position, _speedScore * Time.deltaTime);
        #endregion

    }

    void plav()
    {
        //  Instantiate(goaleffect, goal.gameObject.transform);
        goaleffect.Play();
    }

    void Jump()
    {
        if (!_playerCollider.IsTouchingLayers(JumpLayer))
        {
            runningSFX.Stop();
            return;
        }
        Vector2 jumpVelocityToAdd = new Vector2(0f, jump);
        if (!jumpSFX.isPlaying) jumpSFX.Play();
        if (!kickSFX.isPlaying) kickSFX.Play();
        if (!runningSFX.isPlaying) runningSFX.Play();
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
        _playerRigidbody.velocity = new Vector2(Mathf.Lerp(speed, 0, 1 - (_playerSlowdownTimer / _playerSlowdownTime)), _playerRigidbody.velocity.y);
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
            _kickSFXBool = true;
        }
        else
        {
            if (!kickSFX.isPlaying && _kickSFXBool)
            {
                kickSFX.Play();
                landSFX.Play();
            }
            _targetPosition = foregroundBallPosition;
            _ballTransitionStage -= Time.deltaTime;
            _kickSFXBool = false;
        }
        _ballTransitionStage = Mathf.Clamp(_ballTransitionStage, 0, _ballTransitionSpeed);
        currentBallPosition.transform.position = new Vector2(transform.position.x, _ballYPosition) + Vector2.Lerp(foregroundBallPosition, backgroundBallPosition, _ballTransitionStage / _ballTransitionSpeed);
        float scale = Mathf.Lerp(_ballForegroundScale, _ballBackgroundScale, _ballTransitionStage / _ballTransitionSpeed);
        currentBallPosition.transform.localScale = new Vector3(scale, scale, scale);
        yield return null;
    }
    #endregion

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "block")
        {
            canPlay = false;

            _backgroundPlayerFinalX = backgroundPlayer.transform.position.x;
            runningSFX.Stop();
            backgroundPlayer.GetComponent<Animator>().SetBool("end", true);
            _playerAnimator.SetBool("death", true);
            facePlantSFX.Play();
            _animationDelaytime = AnimatorNextClipLength(fallAnimationName);
            FindObjectOfType<FadInLoading>().SceneToLoad(2);
            FindObjectOfType<FadInLoading>().StartCoroutine(FindObjectOfType<FadInLoading>().LoadingScreem(2, 2));
        }
    }
}
