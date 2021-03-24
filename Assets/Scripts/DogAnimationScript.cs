using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnimationScript : MonoBehaviour
{
    [SerializeField]
    private float checkDistance = 40.0f;

    private Animator anim;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector2.Distance(transform.position, playerTransform.position));
        anim.SetBool("playerNear", CheckDistance(playerTransform, checkDistance));
    }

    bool CheckDistance(Transform _playerTransform, float _desiredDistance)
    {
        if (_playerTransform.gameObject != null)
        {
            if(Vector2.Distance(transform.position, _playerTransform.position) < _desiredDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
