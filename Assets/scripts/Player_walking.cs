using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_walking : MonoBehaviour
{
    public float movementspeed = 1;
    public bool facingRight = true;

    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _playerAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        if (GetComponent<Animator>()) _playerAnimator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerAnimator != null)
        {
            var movement = Input.GetAxis("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementspeed;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -92.5f, 92.5f), transform.position.y, transform.position.z);

            _playerAnimator.SetFloat("speed", Mathf.Abs(movement));

            if (movement > 0) _spriteRenderer.flipX = false;
            else if (movement < 0) _spriteRenderer.flipX = true;
        }
    }
}
