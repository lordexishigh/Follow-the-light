using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Gates : MonoBehaviour
{
    private SpriteRenderer sprR;
    private Light2D light;
    private int count = 0;
    private float currentTime = 0;
    [SerializeField]
    private float timer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        sprR = GetComponent<SpriteRenderer>();
        light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        

        if (currentTime > timer)
        {
            print(currentTime);
            count++;
            if(count > 3)
            {
                count = 0;
            }
            
            RotateColours();
            timer += 2f; 
        }
        
    }

    void RotateColours()
    {
        sprR.color = ColorfulPlayer.colors[count];
        light.color = ColorfulPlayer.colors[count];
        gameObject.layer = count + 9;
    }
}
