using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Put this script on the player, not the gem shard
/*
    Gem Shard stuff:
    1. Make sure the shard's collider is a trigger
    2. Make sure the shard is tagged as "GemShard"
 */

public class GemCollect : MonoBehaviour
{
    public bool gemCollected;

    void Start()
    {
        gemCollected = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "GemShard")
        {
            gemCollected = true;
            Destroy(other.gameObject);
        }
    }
}
