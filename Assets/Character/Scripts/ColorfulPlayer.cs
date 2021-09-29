using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ColorfulPlayer : MonoBehaviour
{
    private static int pickups = 0;
    private SpriteRenderer spriteR;
    private new Light2D light;
    private Color OriginalColor = new Color(1f, 0.64f, 0.09f, 1f); //Original
    private static Color currentColor;
    public static Color[] colors = {
        new Color(0.07843138f, 0.7490196f, 0.1803922f, 1.0f), //Green 14BF2E 
        new Color(0.09019608f, 0.5019608f, 0.9098039f, 1.0f), //Blue 1780E8 
        new Color(0.6313726f, 0.2705882f, 0.9019608f, 1.0f), //Purple A145E6 
        new Color(1f, 0.6392157f, 0.09019608f, 1f) 
        //new Color(1.0f, 1.0f, 1.0f, 1.0f) //White FFFFFF
    }; 

    // Start is called before the first frame update
    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        light = GetComponent<Light2D>();
        if(currentColor == Color.clear)
        {
            currentColor = spriteR.color;
        }

            
        ChangeColor(currentColor);

    }

    public void ChangeColor(Color color)
    {
        spriteR.color = color;
        light.color = color;
        currentColor = color;
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

    public void increaseLight(float amount = 0.4f)
    {
        pickups++;
        light.pointLightOuterRadius += amount;
    }

    public int getPickups()
    {
        return pickups;
    }
}
