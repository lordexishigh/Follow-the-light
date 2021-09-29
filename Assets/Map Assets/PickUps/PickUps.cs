using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUps : MonoBehaviour
{
    private ColorfulPlayer player;
    private Text tex;
    private static bool picked = false;

    // Start is called before the first frame update
    void Start()
    {
        if(picked == true)
        {
            gameObject.SetActive(false);
        }
        GameObject uiText = GameObject.Find("sunText");
        tex = uiText.GetComponent<Text>();

        GameObject _player = GameObject.FindWithTag("Player");
        player = _player.GetComponent<ColorfulPlayer>();

        tex.text = (player.getPickups()).ToString();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            player.increaseLight();
            tex.text = (player.getPickups()).ToString();
            Destroy(gameObject);
        }
    }
}
