using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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
    LayerMask noEnemy;

    // 1 = Stationary, 2 = Rotating, 3 = Patroling, 4 = Wandering
    public int enemyType;

    bool triggerPatrolingStateChange;

    bool moving;

    float chaseSpeed = 40;

    int xMove;
    int yMove;

    // 1 = N, 2 = E, 3 = S, 4 = W
    //int faceDir;

    float maxChaseDist;

    Vector2 homeVector;

    RaycastHit2D hit;

    Vector2 moveVectorRaw;
    Vector2 moveVectorNormalized;

    float moveAngle;

    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        detectScript = GetComponentInChildren<EnemyDetectPlayer>();

        player = GameObject.FindGameObjectWithTag("Player");
        AMScript = player.GetComponent<AdrenalineMode>();

        state = EnemyState.PATROLING;

        noEnemy = LayerMask.GetMask("Enemy");

        triggerPatrolingStateChange = false;

        moving = false;

        maxChaseDist = 10;

        if (enemyType == 1 || enemyType == 2 || enemyType == 4)
        {
            homeVector = new Vector2(tf.position.x, tf.position.y);
            if (enemyType == 1)
            {
                // Call a starting rotation here
            }
        }
        else if (enemyType == 3)
        {

        }
    }

    void Update()
    {
        if (state == EnemyState.PATROLING && triggerPatrolingStateChange)
        {
            triggerPatrolingStateChange = false;

            // Stationary and Rotating (they do mostly the same thing)
            if (enemyType == 1 || enemyType == 2 || enemyType == 4)
            {
                GoToCoord(tf.position.x, tf.position.y, homeVector.x, homeVector.y);

                if (enemyType == 1)
                {
                    // Use the starting rotation here
                }
            }
            // Patroling
            else if (enemyType == 3)
            {
                // Go to the next point on their patrol route
            }
            else if (enemyType != 4)
            {
                Debug.LogError(this.name + " does not have it's enemyType set");
            }
        }
        else if (state == EnemyState.CHASINGPLAYER)
        {
            triggerPatrolingStateChange = true;

            hit = Physics2D.Raycast(transform.position, player.transform.position, maxChaseDist, ~noEnemy);
            
            if (hit.collider == null)
            {
                if (!moving)
                {
                    StartCoroutine(EnemyChaseMove(player.transform.position.x, player.transform.position.y));
                }
            }
            else
            {
                ChaseLastSeenStateEnter();
                state = EnemyState.CHASINGLASTSEEN;
            }

        }
        else if (state == EnemyState.CHASINGLASTSEEN)
        {

        }
    }

    void GoToCoord(float xPosStart, float yPosStart, float xPosFin, float yPosFin)
    {

    }

    IEnumerator EnemyChaseMove(float playerX, float playerY)
    {
        moving = true;

        //Debug.Log("EnemyChaseMove called");

        CalculateMoveDir(playerX, playerY);

        for (int i = 0; i <= chaseSpeed; i++)
        {
            if (playerX == 1)
            {
                tf.position = new Vector2(tf.position.x + (1 / chaseSpeed), tf.position.y);
            }
            else if (playerX == -1)
            {
                tf.position = new Vector2(tf.position.x - (1 / chaseSpeed), tf.position.y);
            }

            if (playerY == 1)
            {
                tf.position = new Vector2(tf.position.x, tf.position.y + (1 / chaseSpeed));
            }
            else if (playerY == -1)
            {
                tf.position = new Vector2(tf.position.x, tf.position.y - (1 / chaseSpeed));
            }

            yield return null;
        }

        tf.position = new Vector2(Mathf.RoundToInt(tf.position.x), Mathf.RoundToInt(tf.position.y));

        ResetVars();

        moving = false;
    }

    void CalculateMoveDir(float targetX, float targetY)
    {
        moveVectorRaw = new Vector2(targetX - transform.position.x,  targetY - transform.position.y);
        moveAngle = Vector2.Angle(Vector2.up, moveVectorRaw);
        //Debug.Log(moveAngle);

        /*
        Angle Notes: value between 0-180
        0 - Straight up
        22.5
        45 - Diagonally up and to either the left or right
        67.5
        90 - Straight left or right
        112.5
        135 - Diagonally down and to either the left or right
        157.5
        180 - Straight down
         */

        if (moveAngle < 22.5)
        {
            xMove = 0;
            yMove = 1;
        }
        else if (22.5 <= moveAngle && moveAngle <= 67.5)
        {
            if (player.transform.position.x > tf.position.x)
            {
                xMove = 1;
            }
            else
            {
                xMove = -1;
            }
            yMove = 1;
        }
        else if (67.5 < moveAngle && moveAngle < 112.5)
        {
            if (player.transform.position.x > tf.position.x)
            {
                xMove = 1;
            }
            else
            {
                xMove = -1;
            }
            yMove = 0;
        }
        else if (112.5 <= moveAngle && moveAngle <= 157.5)
        {
            if (player.transform.position.x > tf.position.x)
            {
                xMove = 1;
            }
            else
            {
                xMove = -1;
            }
            yMove = -1;
        }
        else if (157.5 < moveAngle)
        {
            xMove = 0;
            yMove = -1;
        }
        else
        {
            Debug.LogError("moveAngle not calculated correctly: " + moveAngle);
        }
    }

    void ChaseLastSeenStateEnter()
    {

    }

    void ResetVars()
    {
        xMove = 0;
        yMove = 0;
    }
}
