using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyDetectPlayer : MonoBehaviour
{
    EnemyMove eMoveScript;

    PlayerHideController phc;

    RaycastHit2D hit;

    void Start()
    {
        eMoveScript = GetComponentInParent<EnemyMove>();

        phc = GameObject.FindWithTag("Player").GetComponent<PlayerHideController>();
    }

    // Logic for when the Enemy is in the PATROLING state (or the CHASINGLASTSEEN ig)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && eMoveScript.state != EnemyState.CHASINGPLAYER && !phc.hiding)
        {
            // Check to make sure that the player didn't hit the collider through a wall or something
            hit = Physics2D.Raycast(transform.position, eMoveScript.player.transform.position, Mathf.Infinity, ~eMoveScript.playerAndEnemy);

            if (hit.collider == null)
            {
                Debug.Log("no collider detected");
                eMoveScript.state = EnemyState.CHASINGPLAYER;
            }
            else
            {
                Debug.Log(hit.collider.name);
            }
        }
    }
}
