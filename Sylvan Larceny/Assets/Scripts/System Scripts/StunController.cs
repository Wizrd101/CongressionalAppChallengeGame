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

    Animator anim;

    float playerStunTime;
    float enemyStunTime;

    bool isPlayer;

    public bool stunned;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();

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

        stunned = false;
    }

    public IEnumerator StunThisGO()
    {
        Debug.Log("Stunned: " + this.gameObject.name);

        stunned = true;

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

        stunned = false;
    }
}
