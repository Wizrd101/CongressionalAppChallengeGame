using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseTrapTrigger : MonoBehaviour
{
    AudioSource m_as;

    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    public void NoiseTrapTriggered()
    {
        m_as.Play();

    }
}
