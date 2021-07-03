using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rigidBody;

    private float velocity, maxVelocity, acceleration, dir;

    [Tooltip("Commanded is true if the player instructed the AI to move to a position")]
    public bool commanded;
    [Tooltip("Boost is true if the player instructed the AI to boost him, enabling collision")]
    public bool boost;

    private float comPos;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = rigidBody = GetComponent<Rigidbody2D>();

        comPos = 9999f;

        distance = 2f;
        dir = 0f; //direction

        velocity = 0.001f;
        maxVelocity = 0.05f;

        acceleration = 0.001f;

        commanded = false;
        boost = false;

        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!boost)
        {
            if (commanded && comPos != 9999) { Moving(comPos); }

            else { Moving(player.transform.position.x); }
        }
    }

    void Moving(float target)
    {
        float dis = gameObject.transform.position.x - target;
        if (Mathf.Abs(dis) > distance)
        {
            dir = -(dis / Mathf.Abs(dis));
            if (Mathf.Abs(velocity) < maxVelocity)
            {
                velocity += dir * acceleration;
            }
            rigidBody.velocity = new Vector2(velocity, rigidBody.velocity.y);
            transform.position = transform.position + new Vector3(velocity, 0, 0);
        }
        else { velocity = 0f; }
    }

    public void Commanded(float pos = 9999)
    {
        if (pos != 9999) { 
            commanded = true;
            if (pos < gameObject.transform.position.x) { comPos = pos - distance; }
            else { comPos = pos + distance; }
        }
        else { commanded = !commanded; comPos = 9999; }
    }

    public void Boosted()
    {
        boost = !boost;
        if (boost) { Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false); }

        else { Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>()); }
    }
}
