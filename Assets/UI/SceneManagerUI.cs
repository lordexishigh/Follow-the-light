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

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void RestartScene() 
    {
        ai.FoodReset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            if (SceneManager.GetActiveScene().buildIndex % 2 == 0)
            {
                if (SceneManager.GetActiveScene().buildIndex > ai.getFood())
                {
                    print("not enough food");
                    return;
                }

                if (!ai.getCarriedReached())
                {
                    print("AI not picked");
                    return;
                }
            }

            LoadNextScene(); 
        }
    }
}
