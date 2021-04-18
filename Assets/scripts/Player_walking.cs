using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_walking : MonoBehaviour
{
    public float movementspeed = 1;
    public bool facingRight = true;
    public Canvas canvieBoi;
    public AudioSource walkSFX;

    private float _pos;
    private float _spriteSize;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _playerAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        if (GetComponent<Animator>()) _playerAnimator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteSize = _spriteRenderer.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerAnimator != null && !PauseGame.paused)
        {
            Movement();
        }
    }

    private void Movement()
    {
        if (Input.GetMouseButtonDown(0)) _pos = MousePositionEqualHeight();
        if (Vector2.Distance(new Vector2(_pos, 0), new Vector2(transform.localPosition.x, 0)) > 1f && movementspeed != 0)
        {
            _playerAnimator.SetFloat("speed", 0.8f);
            if (!walkSFX.isPlaying) walkSFX.Play();
        }
        else
        {
            _playerAnimator.SetFloat("speed", 0);
            walkSFX.Stop();
        }
        if (_pos < transform.localPosition.x) _spriteRenderer.flipX = true;
        else _spriteRenderer.flipX = false;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(_pos, transform.localPosition.y, transform.localPosition.z), movementspeed * Time.deltaTime);
        //transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -_spriteSize/2, _spriteSize/2), transform.localPosition.y, transform.localPosition.z);
    }

    private float MousePositionEqualHeight()
    {
        float scaledMousePos = (Input.mousePosition.x - (Screen.width / 2)) / Screen.width;
        scaledMousePos = Mathf.Clamp(scaledMousePos, -0.5f, 0.5f);
        scaledMousePos *= canvieBoi.GetComponent<RectTransform>().rect.width;
        return scaledMousePos;
    }
}
