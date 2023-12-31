using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterGameController : MonoBehaviour
{
    GameObject startGameButton;
    GameObject newGameButton;
    GameObject loadGameButton;

    void Awake()
    {
        startGameButton = GameObject.Find("StartGameButton");
        newGameButton = GameObject.Find("StartNewGameButton");
        loadGameButton = GameObject.Find("LoadGameButton");
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("SaveFileExitsts", 0) != 1)
            loadGameButton.GetComponent<Button>().interactable = false;
        else
            loadGameButton.GetComponent<Button>().interactable = true;

        startGameButton.SetActive(true);
        newGameButton.SetActive(false);
        loadGameButton.SetActive(false);
    }

    public void StartGameButtonClick()
    {
        startGameButton.SetActive(false);
        newGameButton.SetActive(true);
        loadGameButton.SetActive(true);
    }

    public void NewGameButtonClick()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("LevelOneScene");
    }

    public void LoadGameButtonClick()
    {
        SceneManager.LoadScene("MainMenuScece");
    }
}
