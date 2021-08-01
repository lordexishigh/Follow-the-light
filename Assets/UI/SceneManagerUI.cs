using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerUI : MonoBehaviour
{
    private AI ai;

    void Start()
    {
        GameObject _ai = GameObject.FindWithTag("AI");
        ai = _ai.GetComponent<AI>();
    }

    public void LoadNextScene(int food = 0)
    {
        if (food <= ai.getFood())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision);
        if(collision.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex % 5 == 0) {
                LoadNextScene(SceneManager.GetActiveScene().buildIndex);
            }
            else { LoadNextScene(); }
        }
    }
}
