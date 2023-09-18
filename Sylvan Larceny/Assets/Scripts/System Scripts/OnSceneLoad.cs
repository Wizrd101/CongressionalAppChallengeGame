using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneLoad : MonoBehaviour
{
    void Start()
    {
        Physics2D.queriesHitTriggers = false;
    }
}
