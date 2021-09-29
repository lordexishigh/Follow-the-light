using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ColoringGems : MonoBehaviour
{
    private SpriteRenderer spr;
    private ColorfulPlayer player;
    private Light2D playerLight;
    private Light2D gemLight;

    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.FindWithTag("Player");
        player = temp.GetComponent<ColorfulPlayer>();
        playerLight = temp.GetComponent<Light2D>();

        spr = GetComponent<SpriteRenderer>();
        gemLight = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            spr.color = playerLight.color;
            player.ChangeColor(gemLight.color);
            gemLight.color = spr.color;
        }
    }
}
