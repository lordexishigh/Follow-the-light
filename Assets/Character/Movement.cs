using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Movement : MonoBehaviour
{
    private float left, right;

    [SerializeField]
    private float groundAccelaration;
    private float airAcceleration;
    //private float deceleration;

    [SerializeField]
    private float velocity;
    public float maxVelocity;

    private float jumpSpeed;
    [SerializeField]
    public bool JumpAvailable;
    public float WallJumpDirection;

    public bool OnGround;
    public bool OnWall;
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
        WallJumpDirection = 0f;
        velocity = 0.001f;
        maxVelocity = 0.1f;

        groundAccelaration = 0.005f;
        airAcceleration = 0.001f;
        //deceleration = 0.01f;

        jumpSpeed = 10f;
        JumpAvailable = true;
        OnGround = true;
        OnWall = false;
        carries = false;
        expReady = true;
    }

    void FixedUpdate()
    {

        if (!carries)
        {
            left = Input.GetAxis("Left");
            right = Input.GetAxis("Right");

            if (OnGround)
            {
                if (left + right == 0)
                {
                    velocity = 0f;
                }
                else
                {
                    if (left + right > 0 && velocity < 0 || left + right < 0 && velocity > 0)
                    {
                        velocity = 0f;
                    }
                    else if (Mathf.Abs(velocity) > maxVelocity)
                    {
                        velocity -= (left + right) * 0.001f;     ;
                    }
                    else if (Mathf.Abs(velocity) < maxVelocity - 0.002f)
                    {
                        velocity += (left + right) * groundAccelaration;
                    }
                }
            }
            else
            {
                velocity += (left + right) * airAcceleration;
            }
        }
        else
        {
            velocity = 0f;
        }
        UpdatePlayer();


            //if (left + right == 0) //if both inputs or no inputs are pressed
            //{
            //    if (OnGround)
            //    {
            //        velocity = 0f; acceleration = 0.005f;
            //    }
            //}
            //else
            //{
            //    if ((left + right > 0 && velocity < 0) || (left + right < 0 && velocity > 0)) //if velocity goes one direction and input goes the other
            //    {
            //        if (OnGround)
            //        {
            //            velocity = 0f; acceleration = 0.005f;
            //        }
            //        else
            //            velocity += (left + right) * acceleration;
            //    }
            //    else if (Mathf.Abs(velocity) < maxVelocity - 0.002f)
            //    {
            //        velocity += (left + right) * acceleration;
            //    }
            //    else
            //}

            //UpdatePlayer();

        //if (OnGround && velocity > maxVelocity)
        //{
        //    velocity -= 0.001f;
        //}

    }

    public void Jump(float xspeed = 0, float direction = 0)
    {
        if (OnWall)
        {
            xspeed = 0.2f;
            direction = WallJumpDirection;
        }
        else if (xspeed!=0 && ((velocity > 0 && direction < 0) || velocity < 0 && direction > 0)){
            velocity = 0f;
        }
        velocity += direction * xspeed;
        rigidBody.velocity = new Vector2(velocity, jumpSpeed);
        JumpAvailable = false;
        OnGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //while (((left + right) * velocity > maxVelocity)) //this will slow down the player if they went above the max velocity limit during their jump when they land
        //{
        //    velocity -= (left + right) * acceleration / 2;
        //}
        evaluateCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
       // evaluateCollision(collision); //this causes the jump to be reseted after the jump, fix it
    }

    private void evaluateCollision(Collision2D collision)
    {
        bool collided = false;
        
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            //print(normal);
            if (normal.y >= 0)
            {
                if (Mathf.Abs(normal.x) >= 0.9f)
                {
                    velocity = 0f;
                    OnWall = true;
                    JumpAvailable = true;
                    WallJumpDirection = normal.x;
                }
                
                OnGround = true;
                
                if (normal.y >= 0.6f)
                {
                    collided = expReady = JumpAvailable = true;
                    if (collision.gameObject.tag == "MovingPlatform")
                    {
                        gameObject.transform.SetParent(collision.transform);
                    }
                }
            }

            if(Mathf.Abs(normal.x) > 0.85f)
            {
                velocity = 0f;
            }
        }

        //if (collided == false)
        //{
        //    StayStill();
        //}

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            gameObject.transform.SetParent(null);
        }
        OnWall = false;
        //JumpAvailable = false;
    }

    private void UpdatePlayer()
    {
        rigidBody.velocity = new Vector2(velocity, rigidBody.velocity.y);
        transform.position = transform.position + new Vector3(velocity, 0, 0);
    }



}
