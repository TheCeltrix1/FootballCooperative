using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2D : MonoBehaviour
{
    public float speed = 3.0f;
    public float jump = 20.0f;
    public float energy = 3f;
    public LayerMask JumpLayer;
    public Rigidbody2D playerRigidbody;
    public Collider2D playerCollider;
    public player2D play;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if(energy <=0){       
            return;
        }
        energy -= Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            //GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jump);
        }

    }
    void Jump()
    {
        if (!playerCollider.IsTouchingLayers(JumpLayer))
        {
            return;
        }
        Vector2 jumpVelocityToAdd = new Vector2(0f, jump);
        playerRigidbody.velocity += jumpVelocityToAdd;

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
