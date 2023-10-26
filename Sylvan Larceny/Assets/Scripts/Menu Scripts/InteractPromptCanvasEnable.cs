using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractPromptCanvasEnable : MonoBehaviour
{
    [SerializeField] Canvas thisCv;

    InteractPointController ipc;

    TextMeshProUGUI cvTxt;
    [SerializeField] string customText;

    void Start()
    {
        thisCv = GetComponent<Canvas>();

        ipc = GameObject.FindWithTag("Player").GetComponentInChildren<InteractPointController>();

        cvTxt = GetComponentInChildren<TextMeshProUGUI>();

        if (customText.Length != 0)
            cvTxt.text = customText;
    }

    // Update is called once per frame
    void Update()
    {
        if (ipc.ableToInteract && ipc.currentReciever)
        {
            if (ipc.currentReciever.ableToRecieve)
                thisCv.enabled = true;
            else
                thisCv.enabled = false;
        }
        else
        {
            thisCv.enabled = false;
        }
    }
}
