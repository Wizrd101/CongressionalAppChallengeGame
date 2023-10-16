using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoad : MonoBehaviour
{
    Animator gemAnim;

    int tempLevelLeft;

    void Awake()
    {
        gemAnim = GameObject.Find("Gem").GetComponent<Animator>();
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("SaveFileExists", 0) == 0)
            PlayerPrefs.SetInt("SaveFileExists", 1);

        tempLevelLeft = PlayerPrefs.GetInt("LevelLeft");
    }

    public IEnumerator MenuLateStart()
    {
        yield return null;

        UpdateGemAnimation();
    }

    void UpdateGemAnimation()
    {
        if (tempLevelLeft == 0)
        {
            // Tutorial was left
        }
        else
        {
            // A level was left, play the appropriate trigger
        }
    }
}
