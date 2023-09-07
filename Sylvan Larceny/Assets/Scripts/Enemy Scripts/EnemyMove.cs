using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public enum EnemyState { PATROLING, CHASINGPLAYER, CHASINGLASTSEEN}

public class EnemyMove : MonoBehaviour
{
    Transform tf;
    Rigidbody2D rb;

    EnemyDetectPlayer detectScript;

    public GameObject player;
    AdrenalineMode AMScript;

    public EnemyState state;

    public LayerMask playerAndEnemy;

    // 1 = Stationary, 2 = Rotating, 3 = Patroling, 4 = Wandering
    public int enemyType;

    bool moving;

    // 1 = N, 2 = E, 3 = S, 4 = W
    int faceDir;

    RaycastHit2D hit;

    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        detectScript = GetComponentInChildren<EnemyDetectPlayer>();

        player = GameObject.FindGameObjectWithTag("Player");
        AMScript = player.GetComponent<AdrenalineMode>();

        if (tf == null)
        {
            Debug.LogError("EnemyMovement on " + this.name + " script cannot access a Transform");
        }

        if (rb == null)
        {
            Debug.LogError("EnemyMovement on " + this.name + "script cannot access a Rigidbody2D");
        }

        if (detectScript == null)
        {
            Debug.LogError("EnemyMovement on " + this.name + "script cannot access an EnemyDetectPlayer script");
        }

        if (player == null)
        {
            Debug.LogError("EnemyMovement on " + this.name + "script cannot access the player");
        }

        if (AMScript == null)
        {
            Debug.LogError("EnemyMovement on " + this.name + "script cannot access the AMScript");
        }

        state = EnemyState.PATROLING;

        moving = false;

        playerAndEnemy = LayerMask.GetMask("Player");
        playerAndEnemy = LayerMask.GetMask("Enemy");
    }

    void Update()
    {
        if (state == EnemyState.PATROLING)
        {

        }
        else if (state == EnemyState.CHASINGPLAYER)
        {
            hit = Physics2D.Raycast(transform.position, player.transform.position, Mathf.Infinity, ~playerAndEnemy);
            while (hit.collider == null)
            {
                if (!moving)
                {
                    StartCoroutine(EnemyChaseMove(player.transform.position.x, player.transform.position.y));
                }
            }
        }
        else if (state == EnemyState.CHASINGLASTSEEN)
        {

        }
    }

    IEnumerator EnemyPatrolMove()
    {
        moving = true;

        yield return null;

        moving = false;
    }

    IEnumerator EnemyChaseMove(float targetX, float targetY)
    {
        moving = true;

        yield return null;

        moving = false;
    }
}
