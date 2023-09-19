using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class StunController : MonoBehaviour
{
    PlayerMovement pm;
    ThrowRock ptr;

    EnemyMove em;
    EnemyDetectPlayer edp;

    float playerStunTime;
    float enemyStunTime;

    bool isPlayer;

    void Start()
    {
        playerStunTime = 1f;
        enemyStunTime = 1.5f;

        if (this.gameObject.tag == "Player")
        {
            pm = GetComponent<PlayerMovement>();
            ptr = GetComponent<ThrowRock>();

            isPlayer = true;
        }
        else
        {
            em = GetComponent<EnemyMove>();
            edp = GetComponentInChildren<EnemyDetectPlayer>();
            
            isPlayer = false;
        }
    }

    public IEnumerator StunThisGO()
    {
        if (isPlayer)
        {
            pm.enabled = false;
            ptr.enabled = false;
            yield return new WaitForSeconds(playerStunTime);
            pm.enabled = true;
            ptr.enabled = true;
        }
        else
        {
            em.enabled = false;
            edp.enabled = false;
            yield return new WaitForSeconds(enemyStunTime);
            em.enabled = true;
            edp.enabled = true;
        }
    }
}
