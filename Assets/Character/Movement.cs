using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Movement : MonoBehaviour
{
    private float left, right;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private float velocity;
    public float maxVelocity;

    private float jumpSpeed;
    [SerializeField]
    public bool OnGround;

    public bool carries;
    public bool expReady;

    private Rigidbody2D rigidBody;
    private Controller cotr;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        cotr = GetComponent<Controller>();
        Physics.IgnoreLayerCollision(0, 6);

        left = right = 0f;

        velocity = 0.001f;
        maxVelocity = 0.075f;

        acceleration = 0.001f;

        jumpSpeed = 10f;
        OnGround = true;

        carries = false;
        expReady = true;
    }

    void FixedUpdate()
    {
        if (!carries)
        {
            left = Input.GetAxis("Left");
            right = Input.GetAxis("Right");

            if (left + right == 0)
            {
                if (OnGround)
                {
                    velocity = 0f; acceleration = 0.005f;
                }
            }
            else
            {
                if ((((left + right) > 0) && (velocity < 0)) || ((left + right) < 0) && (velocity > 0))
                {
                    if (OnGround)
                    {
                        velocity = 0f; acceleration = 0.005f;
                    }
                    else
                        velocity += (left + right) * acceleration;
                }
                else if (Mathf.Abs(velocity) < maxVelocity)
                {
                    velocity += (left + right) * acceleration;
                }
            }

            rigidBody.velocity = new Vector2(velocity, rigidBody.velocity.y);
            transform.position = transform.position + new Vector3(velocity, 0, 0);
        }
        else
        {
            velocity = 0f;
            rigidBody.velocity = new Vector2(velocity, rigidBody.velocity.y);
            transform.position = transform.position + new Vector3(velocity, 0, 0);
        }

    }

    public void Jump(float xspeed = 0, float direction = 0)
    {
        velocity += direction * xspeed;
        rigidBody.velocity = new Vector2(velocity, jumpSpeed);
        OnGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        while (((left + right) * velocity > maxVelocity)) //this will slow down the player if they went about the max velocity limit during their jump
        {
            velocity -= (left + right) * acceleration / 2;
        }
        evaluateCollision(collision);
    }

    private void evaluateCollision(Collision2D collision)
    {
        bool hit = false;
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            if (normal.y >= 0.7f)
            {

                hit = expReady = OnGround = true;
                if (collision.gameObject.tag == "MovingPlatform")
                {
                    gameObject.transform.SetParent(collision.transform);
                }
            }
        }

        if (hit == false)
        {
            velocity = 0f;
            rigidBody.velocity = new Vector2(velocity, rigidBody.velocity.y);
            transform.position = transform.position + new Vector3(velocity, 0, 0);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            gameObject.transform.SetParent(null);
        }
    }
}
