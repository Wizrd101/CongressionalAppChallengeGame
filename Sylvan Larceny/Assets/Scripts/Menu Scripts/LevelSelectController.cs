using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    void Start()
    {
        
    }

    public void LevelOneSelect()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LevelOneScene");
    }

    public void LevelTwoSelect()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LevelTwoScene");
    }

    public void LevelThreeSelect()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LevelThreeScene");
    }

    public void LevelFourSelect()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LevelFourScene");
    }

    public void LevelFiveSelect()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LevelFiveScene");
    }
}
