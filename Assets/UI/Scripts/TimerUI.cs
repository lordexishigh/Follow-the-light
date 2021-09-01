using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    private static float timer = 0f;
    private static float timeDif = 0f;
    private Text text;
    private bool running;

    private float minutes, seconds;

    // Start is called before the first frame update
    void Start()
    {
        running = true;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            timer += Time.deltaTime;
            UpdateTimer(timer - timeDif);
        }        
    }

    private void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
