using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartButton()
    {
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
