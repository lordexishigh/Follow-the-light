using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Movement mov;
    public GameObject _ai;
    private AI ai;
    public Camera Cam;

    public bool expReady;

    // Start is called before the first frame update
    void Start()
    {
        mov = GetComponent<Movement>();
        ai = _ai.GetComponent<AI>();

        expReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && mov.OnGround)
        {
            mov.Jump();
        }
        else if (Input.GetButtonDown("ExplosiveJumpE") && expReady)
        {
            expReady = false;
            mov.Jump(mov.maxVelocity, 1);
        }
        else if (Input.GetButtonDown("ExplosiveJumpQ") && expReady)
        {
            expReady = false;
            mov.Jump(mov.maxVelocity, -1);
        }

        if (Input.GetButtonDown("Command")) {
            ai.Commanded();
        }

        if (Input.GetMouseButtonDown(0) && ai.commanded)
        {
            ai.Commanded((Camera.main.ScreenToWorldPoint(Input.mousePosition)).x);
        }

        if (Input.GetButtonDown("Boost"))
        {
            ai.Boosted();
        }
    }
}

