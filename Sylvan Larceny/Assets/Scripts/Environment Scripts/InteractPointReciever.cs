using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPointReciever : MonoBehaviour
{
    public bool ableToRecieve;
    public bool interactionInPlace;

    void Start()
    {
        ableToRecieve = true;
        interactionInPlace = false;
    }

    public void RecieveInteractionActivate()
    {
        if (ableToRecieve && !interactionInPlace)
        {
            interactionInPlace = true;

            if (gameObject.tag == "GemFragment")
            {
                DisplayGemController gemFragCv = GameObject.Find("DisplayGemFragmentCanvas").GetComponent<DisplayGemController>();
                StartCoroutine(gemFragCv.TriggerGemGrab());
            }
            else if (gameObject.tag == "MainMenuGem")
            {

            }
            else
            {
                Debug.LogError("InteractionPointReciever on gameObject " + gameObject.name + " could not determine what interaction it has to do. Incorrectly assigned tag.");
            }
        }
    }
}
