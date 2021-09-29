using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Movement : MonoBehaviour
{
    private float input;
    [SerializeField]
    private float groundAccelaration;

    [SerializeField]
    private float velocity;
    public float maxVelocity;

    private float jumpSpeed;
    [SerializeField]
    public bool JumpAvailable;

    public bool OnGround;
    public bool expReady;

    private Rigidbody2D rigidBody;
    private Controller cotr;

    private Vector2 slope;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        cotr = GetComponent<Controller>();
        Physics.IgnoreLayerCollision(0, 6);

        input = 0f;
        velocity = 0.001f;
        maxVelocity = 7.222f;
        slope.Set(1f, 1f);

        groundAccelaration = 0.5f;

        jumpSpeed = 10f;
        JumpAvailable = true;
        OnGround = true;
        expReady = true;
    }

    void FixedUpdate()
    {
        input = Input.GetAxisRaw("Horizontal");

        if (OnGround)
        {
            if ((input == 0) || (input > 0 && velocity <= 0 || input < 0 && velocity >= 0))
            {
                velocity = 0;
            }
        }

        else if (input > 0 && velocity < 0 || input < 0 && velocity > 0) // to allow mid air movement
        {
            velocity += input * groundAccelaration;   
        }

        if (Mathf.Abs(velocity) < maxVelocity)
        {
            velocity += input * groundAccelaration;
        }

        UpdatePlayer();
    }

    public void Jump(float xspeed = 0, float direction = 0)
    {
        if (xspeed != 0 && ((velocity > 0 && direction < 0) || velocity < 0 && direction > 0))
        {
            velocity = 0f;
        }
        velocity += direction * xspeed;
        rigidBody.velocity = new Vector2(velocity, jumpSpeed);
        JumpAvailable = false;
        OnGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        while (input * velocity > maxVelocity) //this will slow down the player if they went above the max velocity limit during their jump when they land
        {
            velocity -= input * groundAccelaration / 2;
        }
        evaluateCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (!Input.GetButton("ExplosiveJumpE") && !Input.GetButton("ExplosiveJumpQ"))
        {
            evaluateCollision(collision);
        }
    }

    private void evaluateCollision(Collision2D collision)
    {
        bool collided = false;

        for (int i = 0; i < collision.contactCount; i++)  
        {
            Vector2 normal = collision.GetContact(i).normal;
            if (normal.y >= 0.6f)
            {
                OnGround = true;
                collided = expReady = JumpAvailable = true;
                if (collision.gameObject.tag == "MovingPlatform")
                {
                    gameObject.transform.SetParent(collision.transform);
                }
            }

            
            if (Mathf.Abs(normal.x) > 0.9f)
            {
                velocity = 0f;
            }
            slope = -input * Vector2.Perpendicular(normal).normalized;

            //print("Angle" + angle);
            Debug.DrawLine(slope + (Vector2)transform.position, transform.position);
           // print("Slope" + slope);
           // print("Normal" + normal);
            //Debug.DrawLine(transform.position, slope);

            if (slope.y == 0f)
            {
                slope.Set(slope.x, 1f);
            }
            if(slope.x <= 0f)
            {
                slope.Set(1f, slope.y);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            gameObject.transform.SetParent(null);
        }
        JumpAvailable = false;
        OnGround = false;
        slope.Set(1f, 1f);
    }

    private void UpdatePlayer()
    {
        rigidBody.velocity = new Vector2(velocity /** slope.x*/, rigidBody.velocity.y /** slope.y*/);
        // transform.position = transform.position + new Vector3(velocity, 0, 0);
    }
}