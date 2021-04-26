using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitPlayer : MonoBehaviour
{
    public float hitTime = 1;
    [HideInInspector] public Transform playerFacePosition;
    private Vector3 _startPos;
    private float _hitTimer = 0;

    private void Start()
    {
        _startPos = transform.localPosition;
    }
    public IEnumerator HitPlayerInTheFace()
    {
        while (_hitTimer < hitTime)
        {
            if (_hitTimer < hitTime / 2)
            {
                transform.position = Vector3.Lerp(transform.position, playerFacePosition.position + new Vector3(0, 1, 0), _hitTimer / (hitTime / 2));
                _hitTimer += Time.deltaTime;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, playerFacePosition.position + _startPos, (_hitTimer - (hitTime / 2)) / (hitTime / 2));
                _hitTimer += Time.deltaTime;
            }
            yield return null;
        }
    }
}
