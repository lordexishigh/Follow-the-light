using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoringGems : MonoBehaviour
{
    private ColorfulPlayer player;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        color = spr.color;
        GameObject temp = GameObject.FindWithTag("Player");
        player = temp.GetComponent<ColorfulPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player.ChangeColor(color);
            Destroy(gameObject);
        }
    }
}
