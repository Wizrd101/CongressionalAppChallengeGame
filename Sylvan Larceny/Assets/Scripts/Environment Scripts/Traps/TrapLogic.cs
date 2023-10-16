using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogic : MonoBehaviour
{
    ArrowTrapTrigger att;
    NoiseTrapTrigger ntt;

    bool trapTriggered;

    void Start()
    {
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

        trapTriggered = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !trapTriggered)
        {
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
    }
}
