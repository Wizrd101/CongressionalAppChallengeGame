using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableThisAnim : MonoBehaviour
{
    Animator thisAnim;

    void Start()
    {
        thisAnim = GetComponent<Animator>();
    }

    public void DisableAnim()
    {
        thisAnim.enabled = false;
    }
}
