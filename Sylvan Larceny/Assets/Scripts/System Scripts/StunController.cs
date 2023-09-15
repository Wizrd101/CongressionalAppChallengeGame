using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class StunController : MonoBehaviour
{
    PlayerMovement pm;
    EnemyMove em;

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

            isPlayer = true;
        }
        else
        {
            em = GetComponent<EnemyMove>();
            
            isPlayer = false;
        }
    }

    public IEnumerator StunThisGO()
    {
        if (isPlayer)
        {
            pm.enabled = false;
            yield return new WaitForSeconds(playerStunTime);
            pm.enabled = true;
        }
        else
        {
            em.enabled = false;
            yield return new WaitForSeconds(enemyStunTime);
            em.enabled = true;
        }
    }
}
