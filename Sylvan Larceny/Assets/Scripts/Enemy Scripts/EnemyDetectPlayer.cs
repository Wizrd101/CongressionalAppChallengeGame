using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyDetectPlayer : MonoBehaviour
{
    PolygonCollider2D detectCol;

    EnemyMove eMoveScript;

    RaycastHit2D hit;

    void Start()
    {
        detectCol = GetComponent<PolygonCollider2D>();

        eMoveScript = GetComponentInParent<EnemyMove>();

        if (detectCol == null)
        {
            Debug.LogError("EnemyDetectPlayer cannot access a PolygonCollider2D");
        }

        if (detectCol == null)
        {
            Debug.LogError("EnemyDetectPlayer cannot access an EnemyMoveScript");
        }
    }

    // Logic for when the Enemy is in the PATROLING state (or the CHASINGLASTSEEN ig)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && eMoveScript.state != EnemyState.CHASINGPLAYER)
        {
            // Check to make sure that the player didn't hit the collider through a wall or something
            hit = Physics2D.Raycast(transform.position, eMoveScript.player.transform.position, Mathf.Infinity, ~eMoveScript.playerAndEnemy);
            
            if (hit.collider == null)
            {
                eMoveScript.state = EnemyState.CHASINGPLAYER;
            }
        }
    }
}
