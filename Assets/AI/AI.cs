using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private GameObject player;
    private Movement mov = null;
    private Rigidbody2D rigidBody;

    private float velocity, maxVelocity, acceleration, dir;

    [Tooltip("Commanded is true if the player instructed the AI to move to a position")]
    public bool commanded;

    [Tooltip("Boost is true if the player instructed the AI to boost him, enabling collision")]
    public bool boost;

    [Tooltip("Carry is true if the player instructed the AI to jump in his packbag")]
    public bool carry;
    private bool carryReached;

    private float comPos;
    private float distance;

    public float food; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();

        comPos = 0f;
        food = 0f;

        distance = 2f;
        dir = 0f; //direction

        velocity = 0.001f;
        maxVelocity = 0.05f;

        acceleration = 0.001f;

        commanded = false;
        boost = false;
        carry = false;
        carryReached = false;

        if (mov == null)
        {
            mov = player.GetComponent<Movement>();
        }

        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame

    void Update()
    {
        if (carryReached) { transform.position = player.transform.position; }
    }

    void FixedUpdate()
    {
        float disx = Mathf.Abs(player.transform.position.x - transform.position.x);
        float disy = Mathf.Abs(player.transform.position.y - transform.position.y);

        if (carry && disx < 2.2f && disy <2.2f)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x - 0.01f, transform.localScale.y - 0.01f, 0);
            }
            else { gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f); }

            if (!carryReached)
            {
                mov.carries = true;
                Moving(new Vector3(player.transform.position.x, transform.position.y,0));
                if (disx < 0.1f)
                {
                    carryReached = true;
                    gameObject.transform.SetParent(player.transform);
                    velocity = 0;
                    rigidBody.velocity = new Vector2(velocity, 0f);
                    mov.carries = false;
                }
                
            }
            
            else { transform.position = player.transform.position; rigidBody.velocity = new Vector2(0f, 0f); }
        }

        else if (!boost)
        {
            if (commanded) { Moving(new Vector3(comPos,transform.position.y,0f)); }

            else 
            {
                Moving(new Vector3(player.transform.position.x, transform.position.y, 0));
            }
        }

        if (transform.localScale.x < 1f && !carry )
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.01f, transform.localScale.y + 0.01f, 0);
        }
    }

    void Moving(Vector3 target)
    {
        float dis = gameObject.transform.position.x - target.x;

        if (Mathf.Abs(dis) > distance)
        {
            dir = -(dis / Mathf.Abs(dis));
            if (Mathf.Abs(velocity) < maxVelocity)
            {
                velocity += dir * acceleration;
            }
            rigidBody.velocity = new Vector2(velocity, rigidBody.velocity.y);
            transform.position = Vector3.MoveTowards(transform.position, target, maxVelocity);
        }

        else { velocity = 0f; }
    }

    public void Commanded(float pos)
    {
        if (!carry)
        {
            commanded = !commanded;
            if (commanded)
            {
                if (pos < gameObject.transform.position.x) { comPos = pos - distance; }
                else { comPos = pos + distance; }
            }
        }
    }

    public void Boosted()
    {
        if (!carry)
        {
            boost = !boost;
            if (boost) { Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false); }

            else { Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>()); }
        }
    }

    public void Carry()
    {
        carry = !carry;
        if (carry)
        {
            boost = false;
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            distance = 0f;
        }

        else
        {
            carryReached = false;
            gameObject.transform.SetParent(null);
            distance = 2f;
        }
    }

    public void FoodPickup()
    {
        food++;
    }

    public float getFood()
    {
        return food;
    }
}
