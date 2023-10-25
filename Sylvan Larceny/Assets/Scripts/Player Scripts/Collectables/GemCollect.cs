using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Put this script on the player, not the gem shard
/*
    Gem Shard stuff:
    1. Make sure the shard's collider is a trigger
    2. Make sure the shard is tagged as "GemShard"
 */

public class GemCollect : MonoBehaviour
{
    [DoNotSerialize] public GemSceneChange gsc;

    public bool gemCollected;

    bool gemPickUpEventTriggered;

    void Start()
    {
        if (GameObject.Find("GemSceneChangeGO").GetComponent<GemSceneChange>())
        {
            gsc = GameObject.Find("GemSceneChangeGO").GetComponent<GemSceneChange>();
        }

        gemCollected = false;
    }

    private void Update()
    {
        if (gsc.doSceneChange && gemCollected && !gemPickUpEventTriggered)
        {
            gemPickUpEventTriggered = true;

            // Add stuff that changes when the gem is picked up
        }
    }
}
