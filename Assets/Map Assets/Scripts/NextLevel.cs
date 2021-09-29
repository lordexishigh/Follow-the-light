using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    private Color color;

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.tag == "Player") 
        {
            SpriteRenderer temp = collision.gameObject.GetComponent<SpriteRenderer>();
            if (temp.color == color)
            {
                SceneManagerUI.LoadNextScene();
            }
            else 
            { 
                print("wrong color"); 
            }
        }
    }
}
