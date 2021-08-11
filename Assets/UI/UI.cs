using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private SceneManagerUI sm;


    void Start()
    {
        GameObject temp = GameObject.Find("nextLevel");
        sm = temp.GetComponent<SceneManagerUI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sm.RestartScene();
        }
    }
}