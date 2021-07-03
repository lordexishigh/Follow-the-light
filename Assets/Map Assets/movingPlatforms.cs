using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatforms : MonoBehaviour
{
    public GameObject loc1;
    public GameObject loc2;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = loc1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameObject.transform.position == target.transform.position)
        {
            if (target == loc1) { target = loc2; }

            else { target = loc1; }
        }

        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 0.03f);
        }
    }
}
