using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsCanvasController : MonoBehaviour
{
    RectTransform settingsPannel;
    bool pannelOnScreen;

    bool buttonsClickable;

    float onScreenPos;
    float offScreenPos;
    [SerializeField] float moveIncrement;

    void Awake()
    {
        settingsPannel = GameObject.Find("SettingsPannel").GetComponent<RectTransform>();
    }

    void Start()
    {
        pannelOnScreen = false;

        onScreenPos = 0;
        offScreenPos = -410;

        if (moveIncrement == 0)
            moveIncrement = 1;
    }

    public void SettingsButtonClick()
    {
        if (buttonsClickable)
        {
            if (pannelOnScreen)
            {

            }
            else
            {

            }
        }
    }

    IEnumerator MovePannelOnScreen()
    {
        while (settingsPannel.position.y < onScreenPos)
        {
            settingsPannel.localPosition += new Vector3(0, moveIncrement, 0);
            yield return null;
        }

        settingsPannel.localPosition = Vector3.zero;
    }

    IEnumerator MovePannelOffScreen()
    {
        while (settingsPannel.position.y > offScreenPos)
        {
            settingsPannel.localPosition -= new Vector3(0, moveIncrement, 0);
            yield return null;
        }

        settingsPannel.localPosition = new Vector3(0, offScreenPos, 0);
    }
}
