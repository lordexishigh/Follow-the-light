using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WhiteGate : MonoBehaviour
{
    private SpriteRenderer spriteR;
    private new Light2D light;

    private void OnCollisionExit2D(Collision2D collision) //gets the color of the player upon colliding (exit)
    { 
        if (collision.gameObject.tag == "Player")
        {
            Color color = collision.gameObject.GetComponent<SpriteRenderer>().color;
            spriteR.color = color;
            light.color = color;
            int count = 0;

            foreach (Color x in ColorfulPlayer.colors)
            {

                if (x == color)
                {
                    gameObject.layer = count + 9;
                }

                count++;
            }
        }
    }
}
