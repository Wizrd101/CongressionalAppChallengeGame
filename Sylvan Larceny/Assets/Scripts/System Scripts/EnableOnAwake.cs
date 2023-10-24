using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnAwake : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToEnable;

    void Awake()
    {
        if (objectsToEnable.Length > 0)
        {
            foreach (GameObject obj in objectsToEnable)
            {
                obj.SetActive(true);
            }
        }
    }
}
