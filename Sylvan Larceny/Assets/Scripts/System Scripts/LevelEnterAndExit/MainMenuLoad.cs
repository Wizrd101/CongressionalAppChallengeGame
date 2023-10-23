using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoad : MonoBehaviour
{
    Animator gScaleImageAnim;
    
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

        gScaleImageAnim = GameObject.Find("PLRCGrayscaleImage").GetComponent<Animator>();

        tempLevelLeft = PlayerPrefs.GetInt("LevelLeft");
    }

    public IEnumerator MenuLateStart()
    {
        yield return null;

        UpdateGemAnimation();
    }

    IEnumerator UpdateGemAnimation()
    {
        

        if (PlayerPrefs.GetInt("LevelSuccess") == 1)
        {
            if (1 <= tempLevelLeft && tempLevelLeft <= 5)
            {
                if (PlayerPrefs.GetInt("LevelsCompleted") < tempLevelLeft)
                {
                    gemAnim.SetInteger("LevelSuccessfullyLeft", tempLevelLeft);
                    gemAnim.SetTrigger("UpdateGem");
                    PlayerPrefs.SetInt("LevelsCompleted", tempLevelLeft);
                }
            }
        }

        yield return new WaitForSeconds(1);
    }
}
