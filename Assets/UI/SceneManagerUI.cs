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
        if (_ai != null)
        {
            ai = _ai.GetComponent<AI>();
        }
    }

    public void LoadNextScene(int neededFood = 0)
    {
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            if (neededFood > ai.getFood())
            {
                print("not enough food");
                return;
            }
            if (ai.getCarriedReached())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else { print("AI not picked"); }
        }
        else { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
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

        if (collision.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex % 2 == 0) {
                LoadNextScene(SceneManager.GetActiveScene().buildIndex);
            }
            else { LoadNextScene(0); }
        }
    }
}
