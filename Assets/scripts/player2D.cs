using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2D : MonoBehaviour
{
    #region oldversion
    /*#region Variables
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
    public GameObject shadowObj;
    private Vector3 _shadowPos;
    private Vector3 _playerPos;
    private float _shadowPosYCalculated;
    private SpriteRenderer _playerRenderer;
    private float _shadowSpriteWidth = 1;
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
    private float _playerSlowdownTime = 1.5f;
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
        _playerRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _playerPos = transform.GetChild(0).transform.position;
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<Collider2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerAnimator.SetBool("noStamina", false);
        _ballYPosition = currentBallPosition.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        _backgroundPlayerY = (backgroundPlayer.GetComponent<SpriteRenderer>().bounds.size.y / 1.5f) + (backgroundBallPosition.y / 1.5f);
        _backgroundPlayerX = backgroundBallPosition.x - (backgroundPlayer.GetComponent<SpriteRenderer>().bounds.size.x / 3);
        _backgroundPlayerFinalX = 0;

        _shadowSpriteWidth = 1;
        _shadowPos = shadowObj.transform.localPosition;
        _shadowPosYCalculated = transform.position.y + _shadowPos.y;
    }

    void Update()
    {
        if (stamina <= 0 || _endAnimation)
        {
            canPlay = false;
            runningSFX.Stop();
            backgroundPlayer.GetComponent<Animator>().SetBool("end", true);
            if (_backgroundPlayerFinalX == 0)
            {
                _backgroundPlayerFinalX = backgroundPlayer.transform.position.x;
            }
            backgroundPlayer.transform.position = new Vector2(_backgroundPlayerFinalX, _backgroundPlayerY);
            backgroundBallPosition.x = backgroundPlayer.transform.localPosition.x;
        }
        else
        {
            backgroundPlayer.transform.position = new Vector2(transform.position.x + _backgroundPlayerX, _backgroundPlayerY);
        }
        #region Stamina Management
        if (stamina <= 0 && _currentMaxStamina < _totalMaxStamina && !_endAnimation)
        {
            canPlay = false;
            if (_playerCollider.IsTouchingLayers(JumpLayer))
            {
                float loadTime;
                _endAnimation = true;
                deepBreathsSFX.Play();
                _endAnimationRandomBool = (Random.Range(0, 2) == 1);
                _playerAnimator.SetBool("randomEndAnimation", _endAnimationRandomBool);
                _playerAnimator.SetBool("noStamina", true);
                _playerAnimator.SetBool("maxStamina", false);
                StartCoroutine(StopMoving(0.5f));
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
            }
            return;
        }
        else if (stamina <= 0 && _currentMaxStamina >= _totalMaxStamina && !_endAnimation)
        {
            canPlay = false;
            if (_playerCollider.IsTouchingLayers(JumpLayer))
            {
                StartCoroutine(StopMoving(0.5f));
                _endAnimation = true;
            }
            return;
        }
        #endregion

        transform.localRotation = Quaternion.Euler(0, 0, 0);

        #region GameEnd
        if (!_endAnimation)
        {
            _playerRigidbody.velocity = new Vector2(speed, _playerRigidbody.velocity.y);
        }
        #endregion

        //Animation
        _playerAnimator.SetFloat("speed", speed);
        _playerAnimator.SetBool("isGrounded", _playerCollider.IsTouchingLayers(JumpLayer));

        #region Movement
        if (Input.GetMouseButtonDown(0) && !_endAnimation && canPlay)
        {
            Jump();
            jumping = true;
        }
        else jumping = false;
        #endregion

        if (!_playerRenderer.sprite.name.Contains("man_sport_fall")) _shadowSpriteWidth = 2;
        else _shadowSpriteWidth = 5;
        ScaleShadow((transform.GetChild(0).position.y + 1.5f) - _playerPos.y);

        #region Kick to background
        if (ballMove) StartCoroutine("PassBall");
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

    void ScaleShadow(float scale)
    {
        shadowObj.transform.position = new Vector3(transform.position.x + _shadowPos.x, _shadowPosYCalculated, _shadowPos.z);
        shadowObj.transform.localScale = new Vector3(_shadowSpriteWidth / scale, shadowObj.transform.localScale.y, 1);
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
    private IEnumerator StopMoving(float stopTime = 1f)
    {
        _playerSlowdownTimer = stopTime;
        while (true)
        {
            _playerRigidbody.velocity = new Vector2(Mathf.Lerp(speed, 0, 1 - (_playerSlowdownTimer / stopTime)), _playerRigidbody.velocity.y);
            _playerSlowdownTimer -= Time.deltaTime;
            if (_playerRigidbody.velocity.magnitude == 0)
            {
                if (_endAnimation && _currentMaxStamina >= _totalMaxStamina && stamina <= 0)
                {
                    finalKickSFX.Play();
                    _playerAnimator.SetBool("noStamina", true);
                    _playerAnimator.SetBool("maxStamina", true);
                    _animationDelaytime = AnimatorNextClipLength(kickAnimationName);
                    goalieAnimator.SetTrigger("Jump");

                    ballMove = false;
                    cheerSFX.PlayDelayed(cheerdelay);
                    plav();
                    GameManager.instance.endgame(2f);
                    break;
                }
                StopCoroutine("StopMoving");
            }
            yield return null;
        }
    }

    private IEnumerator PassBall()
    {
        if (stamina > 0 && canPlay) {
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
            _endAnimation = true;
            canPlay = false;
            _backgroundPlayerFinalX = backgroundPlayer.transform.position.x;
            backgroundPlayer.GetComponent<Animator>().SetBool("end", true);
            StartCoroutine(StopMoving(2));
            GameManager.instance.running = false;
            _playerAnimator.SetBool("death", true);
            facePlantSFX.Play();
            runningSFX.Stop();
            _animationDelaytime = AnimatorNextClipLength(fallAnimationName);
            FindObjectOfType<FadInLoading>().SceneToLoad(2);
            FindObjectOfType<FadInLoading>().StartCoroutine(FindObjectOfType<FadInLoading>().LoadingScreem(2, 2));
        }
    }*/
    #endregion

    #region Variables
    public float speed = 3;
    public float stamina = 5;
    public float startPos;
    public float endPos;
    public LayerMask JumpLayer;
    public Vector3 backgroundBallPosition = new Vector3(2, 1, 0);
    public Vector3 foregroundBallPosition = new Vector3(1, 0, 0);
    public GameObject backGroundPlayer;
    public GameObject faceBallHit;
    public GameObject currentBallPosition;
    public GameObject shadowObj;
    public Transform goal;

    private float _loadTime;
    private float _jumpForceValue = 5;
    private float _totalMaxStamina;
    private bool _inAir = true;
    private bool _randomEndAnimation;
    private bool _animationPlayOnHitGround = false;
    private float _ballTransitionStage;
    private float _ballTransitionSpeed = 0.25f;
    private float _ballBackgroundScale = ((float)(2f / 3f) * 2f);
    private float _ballForegroundScale = 2f;
    private float _ballYPosition;
    private float _backgroundPlayerY;
    private float _backgroundPlayerX;
    private float _shadowSpriteWidth = 1;
    private float _shadowPosYCalculated;
    private float _maxStaminaAtStartOfRun;
    private Vector3 _shadowPos;
    private Vector2 _targetPosition;
    private Vector3 _playerPos;

    //Components
    public ParticleSystem goaleffect;
    public Animator goalieAnimator;
    private Animator _bGPlayerAnimator;
    private SpriteRenderer _playerRenderer;
    private Rigidbody2D _playerRigidbody;
    private Collider2D _playerCollider;
    private Animator _playerAnimator;

    // SFX
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

    //states and state variables
    public static bool k_canPlay = false;
    private bool _canMove = false;
    private float _playerSlowdownTimer;
    #endregion

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();

        #region Declaring Variables
        _randomEndAnimation = (int)Random.Range(0, 2) == 1;
        _ballYPosition = currentBallPosition.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        _totalMaxStamina = GameManager.maxenergy;
        startPos = transform.position.x;
        endPos = transform.position.x + (speed * _totalMaxStamina);
        _playerAnimator = GetComponent<Animator>();
        _playerCollider = GetComponent<Collider2D>();
        _playerPos = transform.GetChild(0).transform.position;
        _playerRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _backgroundPlayerY = (backGroundPlayer.GetComponent<SpriteRenderer>().bounds.size.y / 1.5f) + (backgroundBallPosition.y / 1.5f);
        _backgroundPlayerX = backgroundBallPosition.x - (backGroundPlayer.GetComponent<SpriteRenderer>().bounds.size.x / 3);
        _bGPlayerAnimator = backGroundPlayer.GetComponent<Animator>();
        _shadowPos = shadowObj.transform.localPosition;
        _shadowPosYCalculated = transform.position.y + _shadowPos.y;
        _maxStaminaAtStartOfRun = GameManager.currentMaxStamina;
        #endregion
    }

    private void Update()
    {
        #region movement
        if (k_canPlay)
        {
            _playerRigidbody.velocity = new Vector2(speed, _playerRigidbody.velocity.y);

            if (_playerCollider.IsTouchingLayers(JumpLayer) && _playerRigidbody.velocity.y == 0 && (endPos - transform.position.x) >= 10 && !_inAir) Jump();
            if (_playerCollider.IsTouchingLayers(JumpLayer)) Animations("Land");
            else Animations("Jump");

            StartCoroutine(PassBall());
        }
        if (_playerCollider.IsTouchingLayers(JumpLayer))
        {
            if (_inAir) Land();
            _playerAnimator.SetBool("isGrounded", true);
            if (_animationPlayOnHitGround)
            {
                StartCoroutine(StopMoving(.5f));
                Animations("OutOfStamina");
                SFX("OutOfStamina");
                GameManager.instance.ReturnHome();
                FindObjectOfType<FadInLoading>().SceneToLoad(1);
                FindObjectOfType<FadInLoading>().StartCoroutine(FindObjectOfType<FadInLoading>().LoadingScreem(_loadTime, 1));
                _animationPlayOnHitGround = false;
            }
            _inAir = false;
        }
        else
        {
            _playerAnimator.SetBool("isGrounded", false);
            _inAir = true;
        }
        if (!_bGPlayerAnimator.GetBool("end")) backGroundPlayer.transform.position = new Vector2(transform.position.x + _backgroundPlayerX, _backgroundPlayerY);
        #endregion
        if (!_playerRenderer.sprite.name.Contains("man_sport_fall")) _shadowSpriteWidth = 2;
        else _shadowSpriteWidth = 5;
        ScaleShadow((transform.GetChild(0).position.y + 1.5f) - _playerPos.y);
        #region Stamina Management
        if (stamina <= 0)
        {
            if (_maxStaminaAtStartOfRun >= GameManager.maxenergy)
            {
                currentBallPosition.transform.position = Vector2.MoveTowards(currentBallPosition.transform.position, goal.position, 80 * Time.deltaTime);
            }
            DepleteStamina();
        }
        #endregion
    }

    #region Functions
    private void DepleteStamina()
    {
        if (k_canPlay && _canMove && PauseGameCoOp.tutorialPlayed)
        {
            if (GameManager.currentMaxStamina >= GameManager.maxenergy) Goal();
            else LoadingScene();
            k_canPlay = false;
        }
    }

    private void LoadingScene()
    {
        Animations("BGPlayerDeath");
        _animationPlayOnHitGround = true;
    }

    private void Goal()
    {
        float time = 0.01f;
        StartCoroutine(StopMoving(time));
        Animations("Goal");
        Animations("BGPlayerDeath");
        SFX("Goal");
        GameManager.instance.endgame(2f);
    }

    private void Jump()
    {
        if (Input.GetMouseButtonDown(0) && !_inAir)
        {
            _playerRigidbody.velocity = new Vector2(speed, _jumpForceValue);
            SFX("Jump");
        }
    }

    private void Land()
    {
        SFX("Land");
    }

    private void Animations(string switchValue)
    {
        switch (switchValue)
        {
            case "Jump":
                break;

            case "Land":
                _playerAnimator.SetFloat("speed", speed);
                _canMove = true;
                break;

            case "BGPlayerDeath":
                currentBallPosition.transform.SetParent(null);
                backGroundPlayer.transform.SetParent(null);
                _bGPlayerAnimator.SetBool("end", true);
                break;

            case "PlayerDeath":
                _playerAnimator.SetBool("death", true);
                break;

            case "OutOfStamina":
                _playerAnimator.SetBool("randomEndAnimation", _randomEndAnimation);
                _playerAnimator.SetBool("noStamina", true);
                break;

            case "Goal":
                _playerAnimator.SetBool("maxStamina", true);
                _playerAnimator.SetBool("noStamina", true);
                goalieAnimator.SetTrigger("Jump");
                goaleffect.Play();
                break;
        }
    }

    public void SFX(string switchValue)
    {
        switch (switchValue)
        {
            case "Death":
                facePlantSFX.Play();
                runningSFX.Stop();
                break;

            case "OutOfStamina":
                if (_randomEndAnimation)
                {
                    deepBreathsSFX.Play();
                    deepBreathsSFX.loop = true;
                    _loadTime = 3;
                }
                else
                {
                    faceHitSFX.Play();
                    faceBallHit.GetComponent<BallHitPlayer>().playerFacePosition = transform;
                    faceBallHit.GetComponent<BallHitPlayer>().StartCoroutine("HitPlayerInTheFace");
                    _loadTime = 0.5f;
                }
                runningSFX.Stop();
                break;

            case "Jump":
                if (!jumpSFX.isPlaying) jumpSFX.Play();
                kickSFX.Play();
                runningSFX.Stop();
                break;

            case "Land":
                if (jumpSFX.isPlaying) jumpSFX.Stop();
                landSFX.Play();
                kickSFX.Play();
                runningSFX.Play();
                break;

            case "Goal":
                finalKickSFX.Play();
                cheerSFX.PlayDelayed(1);
                break;
        }
    }

    void ScaleShadow(float scale)
    {
        shadowObj.transform.position = new Vector3(transform.position.x + _shadowPos.x, _shadowPosYCalculated, _shadowPos.z);
        shadowObj.transform.localScale = new Vector3(_shadowSpriteWidth / scale, shadowObj.transform.localScale.y, 1);
    }
    #endregion

    #region Coroutines
    private IEnumerator StopMoving(float stopTime = 1f)
    {
        _playerSlowdownTimer = stopTime;
        while (true)
        {
            if (_playerRigidbody.velocity.magnitude == 0 || !_canMove) break;
            _playerRigidbody.velocity = new Vector2(Mathf.Lerp(speed, 0, 1 - (_playerSlowdownTimer / stopTime)), _playerRigidbody.velocity.y);
            _playerSlowdownTimer -= Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator PassBall()
    {
        if (stamina > 0 && k_canPlay)
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
            k_canPlay = false;
            StartCoroutine(StopMoving(2));
            GameManager.instance.running = false;
            _animationPlayOnHitGround = false;
            Animations("PlayerDeath");
            Animations("BGPlayerDeath");
            SFX("Death");
            FindObjectOfType<FadInLoading>().SceneToLoad(2);
            FindObjectOfType<FadInLoading>().StartCoroutine(FindObjectOfType<FadInLoading>().LoadingScreem(2, 2));
        }
    }
}
