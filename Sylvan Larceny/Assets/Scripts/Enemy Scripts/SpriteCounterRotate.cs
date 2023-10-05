using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to attatch to a sprite, to allow the parent to rotate while the sprite stays in place to keep the animations the same

public class SpriteCounterRotate : MonoBehaviour
{
    Transform thisGO;

    void Start()
    {
        thisGO = GetComponent<Transform>();
    }

    void Update()
    {
        thisGO.eulerAngles = new Vector3(0, 0, 0);
    }
}
