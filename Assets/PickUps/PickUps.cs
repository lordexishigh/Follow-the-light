using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUps : MonoBehaviour
{
    private AI ai;
   // private GameObject uiText;
    private Text tex;

    // Start is called before the first frame update
    void Start()
    {
        GameObject uiText = GameObject.Find("FoodText");
        tex = uiText.GetComponent<Text>();
        GameObject _ai = GameObject.FindWithTag("AI");
        ai = _ai.GetComponent<AI>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            ai.FoodPickup();
            tex.text = ai.food.ToString();
            Destroy(gameObject);
        }
    }
}
