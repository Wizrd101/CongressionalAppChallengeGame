using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LevelOneScene");
    }

    public void LevelSelectButton()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
