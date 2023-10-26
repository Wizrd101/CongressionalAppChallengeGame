using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectCanvasController : MonoBehaviour
{
    Animator anim;

    List<Button> levelButtons = new List<Button>();

    public bool isEnabled;

    void Start()
    {
        anim = GetComponent<Animator>();

        foreach (Button button in GetComponentsInChildren<Button>())
        {
            levelButtons.Add(button);
            button.interactable = false;
        }
    }

    public void EnableButtons()
    {
        foreach (Button button in levelButtons)
        {
            button.interactable = true;
        }
    }

    public void DisableButtons()
    {
        foreach (Button button in levelButtons)
        {
            button.interactable = false;
        }
    }

    public void SlideOn()
    {
        anim.SetTrigger("Start");

        isEnabled = true;
    }

    public void SlideOff()
    {
        anim.SetTrigger("End");

        isEnabled = false;
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
