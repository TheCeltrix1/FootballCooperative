using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnimationScript : MonoBehaviour
{
    [SerializeField]
    private float checkDistance = 80.0f;

    private Animator _anim;
    public Transform playerTransform;
    public AudioSource panting;

    // Start is called before the first frame update
    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("playerNear", CheckDistance(playerTransform, checkDistance));
    }

    bool CheckDistance(Transform _playerTransform, float _desiredDistance)
    {
        if (_playerTransform.gameObject != null)
        {
            if (Vector2.Distance(transform.position, _playerTransform.position) < _desiredDistance)
            {
                if (!panting.isPlaying) panting.Play();
                panting.loop = true;
                return true;
            }
            else
            {
                panting.Stop();
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
