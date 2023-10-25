using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPromptCanvasEnable : MonoBehaviour
{
    [SerializeField] Canvas thisCv;

    InteractPointController ipc;

    void Start()
    {
        thisCv = GetComponent<Canvas>();

        ipc = GameObject.FindWithTag("Player").GetComponentInChildren<InteractPointController>();
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
