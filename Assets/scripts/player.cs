using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Rigidbody box;
    public SphereCollider col;
    public LayerMask groundlayer;
    public float forwardspeed = 2000f;
    public float sideways = 500f;
    public float jump = 500f;
    AudioSource audi;


    void Start()
    {
        col = GetComponent<SphereCollider>();
        audi = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        box.AddForce(0, 0, forwardspeed * Time.deltaTime);

        if (Input.GetKey("d"))
        {
            box.AddForce(sideways * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a"))
        {
            box.AddForce(-sideways * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (IsGrounded() && Input.GetKey("space"))
        {
            box.AddForce(0, jump, 0, ForceMode.Impulse);
            //  audi.Play();
        }

        if (box.position.y < -1f)
        {
            FindObjectOfType<GameManager>().death(2);
        }
    }
    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundlayer);

    }
}
