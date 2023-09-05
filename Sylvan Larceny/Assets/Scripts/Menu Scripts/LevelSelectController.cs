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
        SceneManager.LoadScene("LevelOneScene");
    }

    public void LevelTwoSelect()
    {
        SceneManager.LoadScene("LevelTwoScene");
    }

    public void LevelThreeSelect()
    {
        SceneManager.LoadScene("LevelThreeScene");
    }

    public void LevelFourSelect()
    {
        SceneManager.LoadScene("LevelFourScene");
    }

    public void LevelFiveSelect()
    {
        SceneManager.LoadScene("LevelFiveScene");
    }
}
