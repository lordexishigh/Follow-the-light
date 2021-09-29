using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerUI : MonoBehaviour
{
    private GameObject pauseP;

    private bool paused;

    void Start()
    {
        pauseP = gameObject.transform.Find("PausePanel").gameObject; 
        pauseP.SetActive(false);
        paused = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (paused)
        {
            Time.timeScale = 1;
            pauseP.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pauseP.SetActive(true);
        }
        paused = !paused;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseP.SetActive(false);
        paused = false;
    }

    public static void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public static void RestartScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public static void QuitButton()
    {
        Application.Quit();
    }
}
