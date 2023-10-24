using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCatchPlayer : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Player Loses
        }
    }
}
