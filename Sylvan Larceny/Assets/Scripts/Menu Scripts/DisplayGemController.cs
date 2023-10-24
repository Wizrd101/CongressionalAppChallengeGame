using NUnit.Framework.Internal.Filters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGemController : MonoBehaviour
{
    Canvas thisCv;

    GameObject player;
    GameObject gemFragment;

    Image backgroundImage;
    Image fragmentImage;

    Animator backgroundImageAnim;
    Animator fragmentImageAnim;

    bool interactionTriggered;
    bool startAnimsFinished;
    bool interactionFinished;
    bool endAnimsFinished;

    bool freezeStartTriggered;
    bool freezeEndTriggered;

    void Start()
    {
        thisCv = GetComponent<Canvas>();
        
        player = GameObject.FindWithTag("Player");
        gemFragment = GameObject.FindWithTag("GemFragment");

        backgroundImage = transform.GetChild(0).GetComponent<Image>();
        fragmentImage = transform.GetChild(1).GetComponent<Image>();

        backgroundImageAnim = transform.GetChild(0).GetComponent<Animator>();
        fragmentImageAnim = transform.GetChild(1).GetComponent<Animator>();

        thisCv.enabled = false;

        fragmentImage.sprite = gemFragment.GetComponent<SpriteRenderer>().sprite;
        
        interactionTriggered = false;
        startAnimsFinished = false;
        interactionFinished = false;
        endAnimsFinished = false;
    }

    void Update()
    {
        if (interactionTriggered && !freezeStartTriggered)
        {
            freezeStartTriggered = true;

            player.GetComponent<GemCollect>().gemCollected = true;

            player.GetComponent<PlayerMovement>().playerCanMove = false;
        }

        if (freezeStartTriggered && endAnimsFinished)
        {

        }
    }

    public IEnumerator TriggerGemGrab()
    {
        if (!interactionTriggered)
        {
            interactionTriggered = true;

            thisCv.enabled = true;

            backgroundImageAnim.SetTrigger("Start");
            yield return new WaitForSecondsRealtime(0.75f);
            fragmentImageAnim.SetTrigger("Start");

            yield return new WaitForSecondsRealtime(1f);
            startAnimsFinished = true;
        }
    }

    public IEnumerator TriggerAnimEnd()
    {
        if (startAnimsFinished && !interactionFinished)
        {
            interactionFinished = true;

            fragmentImageAnim.SetTrigger("End");
            yield return new WaitForSecondsRealtime(0.75f);
            backgroundImageAnim.SetTrigger("End");

            yield return new WaitForSecondsRealtime(1f);
            thisCv.enabled = false;

            endAnimsFinished = true;
        }
    }
}
