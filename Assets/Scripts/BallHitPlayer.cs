using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitPlayer : MonoBehaviour
{
    public float hitTime = 2f;
    [HideInInspector] public Transform playerFacePosition;
    private float _hitTimer = 0;
    public IEnumerator HitPlayerInTheFace()
    {
        while (_hitTimer < hitTime)
        {
            transform.position = Vector3.Lerp(transform.position, playerFacePosition.position, _hitTimer/hitTime);
            _hitTimer += Time.deltaTime;
            yield return null;
        }
    }
}
