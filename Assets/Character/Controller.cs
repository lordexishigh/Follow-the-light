using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Movement mov;
    //public GameObject _ai;
    private AI ai;
    private PostProcessing cam;

    // Start is called before the first frame update
    void Start()
    {
        GameObject _ai = GameObject.FindWithTag("AI");
        GameObject _cam = GameObject.FindWithTag("MainCamera");
        
        ai = _ai.GetComponent<AI>();
        cam = _cam.GetComponent<PostProcessing>();

        mov = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && mov.OnGround)
        {
            mov.Jump();
        }
        else if (Input.GetButtonDown("ExplosiveJumpE") && mov.expReady)
        {
            mov.expReady = false;
            mov.Jump(mov.maxVelocity, 1);
        }
        else if (Input.GetButtonDown("ExplosiveJumpQ") && mov.expReady)
        {
            mov.expReady = false;
            mov.Jump(mov.maxVelocity, -1);
        }

        else if (Input.GetButtonDown("Command")) {
            ai.Commanded((Camera.main.ScreenToWorldPoint(Input.mousePosition)).x);
        }

        else if (Input.GetButtonDown("Carry"))
        {
            ai.Carry();
        }

        else if (Input.GetButtonDown("Boost"))
        {
            ai.Boosted();
        }

        else if (Input.GetButtonDown("Drink"))
        {
            cam.Drink();
        }
    }
}

