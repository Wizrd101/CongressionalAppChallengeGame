using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    Canvas pauseCv;

    public bool gamePaused;

    public bool gamePausable;

    void Start()
    {
        pauseCv = GetComponent<Canvas>();

        pauseCv.enabled = false;

        gamePaused = false;

        gamePausable = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeButton()
    {
        UnpauseGame();
    }

    public void RestartButton()
    {
        UnpauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    void PauseGame()
    {
        if (gamePausable)
        {
            Time.timeScale = 1.0f;
            pauseCv.enabled = true;
            gamePaused = false;
        }
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1.0f;
        pauseCv.enabled = false;
        gamePaused = false;
    }
}
