using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private SceneManagerUI sceneMan;

    // Start is called before the first frame update
    void Start()
    {
        GameObject _can = GameObject.Find("Canvas");

        if (_can != null)
        {
            sceneMan = _can.GetComponent<SceneManagerUI>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            sceneMan.LoadNextScene();
        }
    }
}
