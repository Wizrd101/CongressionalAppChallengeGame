using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogic : MonoBehaviour
{
    BoxCollider2D triggerCol;

    ArrowTrapTrigger att;
    NoiseTrapTrigger ntt;

    public bool trapTriggered;

    void Start()
    {
        triggerCol = GetComponent<BoxCollider2D>();

        att = GetComponent<ArrowTrapTrigger>();
        ntt = GetComponent<NoiseTrapTrigger>();

        if (att != null && ntt != null)
        {
            Debug.LogWarning("Trap: " + this.gameObject.name + " has both an ArrowTrapTrigger and a NoiseTrapTrigger attatched");
        }
        else if (att == null &&  ntt == null)
        {
            Debug.LogError("Trap: " + this.gameObject.name + " has neither an ArrowTrapTrigger and a NoiseTrapTrigger attatched");
        }

        triggerCol.enabled = true;

        trapTriggered = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Rock") && !trapTriggered)
        {
            Debug.Log("Trap Triggered");
            trapTriggered = true;
            if (att != null)
            {
                att.ArrowTrapTriggered();
            }
            else if (ntt != null)
            {
                ntt.NoiseTrapTriggered();
            }
        }

        triggerCol.enabled = false;
    }
}
