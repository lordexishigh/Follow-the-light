using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ColorfulPlayer : MonoBehaviour
{
    private SpriteRenderer spriteR;
    private Light2D light;
    private Color OriginalColor = new Color(1f, 0.64f, 0.09f, 1f); //Original
    public static Color[] colors = { 
        new Color(0.21f, 0.90f, 0.45f, 1.0f), //Green 36E673
        new Color(0.30f, 0.60f, 0.90f, 1.0f), //Blue 4C99E6
        new Color(0.64f, 0.28f, 0.90f, 1.0f), //Purple A347E6
        new Color(1.0f, 1.0f, 1.0f, 1.0f) //White FFFFFF
    }; 

    // Start is called before the first frame update
    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        light = GetComponent<Light2D>();
    }

    public void ChangeColor(Color color)
    {
        spriteR.color = color;
        light.color = color;
        int count = 0;
        foreach (Color x in colors)
        {
            if(x == color)
            {
                Physics2D.IgnoreLayerCollision(0, count + 9);
            }
            else 
            {
                Physics2D.IgnoreLayerCollision(0, count + 9, false);
            }
            count++;
        }
    }
}
