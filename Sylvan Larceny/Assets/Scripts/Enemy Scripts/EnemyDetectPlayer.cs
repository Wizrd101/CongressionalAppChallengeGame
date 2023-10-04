using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyDetectPlayer : MonoBehaviour
{
    EnemyAI eAIScript;

    PlayerHideController phc;

    RaycastHit2D hit;

    void Start()
    {
        eAIScript = GetComponentInParent<EnemyAI>();

        phc = GameObject.FindWithTag("Player").GetComponent<PlayerHideController>();
    }

    // Logic for when the Enemy is in the PATROLING state (or the CHASINGLASTSEEN ig)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && eAIScript.enemyState != EnemyAIState.ChasingPlayer && !phc.hiding)
        {
            // Check to make sure that the player didn't hit the collider through a wall or something
            hit = Physics2D.Raycast(transform.position, eAIScript.player.transform.position, Mathf.Infinity, ~eAIScript.playerAndEnemyLayerMask);

            if (hit.collider == null)
            {
                Debug.Log("no collider detected");
                eAIScript.enemyState = EnemyAIState.ChasingPlayer;
            }
            else
            {
                Debug.Log(hit.collider.name);
            }
        }
    }
}
