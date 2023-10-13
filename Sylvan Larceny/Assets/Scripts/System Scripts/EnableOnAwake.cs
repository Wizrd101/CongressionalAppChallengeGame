using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnAwake : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(true);
    }
}
