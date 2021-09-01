using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (paused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            paused = !paused;
        }
    }
}
