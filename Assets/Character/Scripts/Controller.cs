using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Movement mov;
    private PostProcessing cam;

    // Start is called before the first frame update
    void Start()
    {
        GameObject _cam = GameObject.FindWithTag("MainCamera");

        if (_cam != null)
        {
            cam = _cam.GetComponent<PostProcessing>();
        }

        mov = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && mov.JumpAvailable)
        {
            mov.JumpAvailable = false;
            mov.Jump();
        }
        else if (Input.GetButtonDown("ExplosiveJumpE") && mov.expReady)
        {
            mov.expReady = false;
            mov.Jump(3f, 1);
        }
        else if (Input.GetButtonDown("ExplosiveJumpQ") && mov.expReady)
        {
            mov.expReady = false;
            mov.Jump(3f, -1);
        }
        //else if (Input.GetButtonDown("Drink"))
        //{
        //    cam.Drink();
        //}
    }
}

