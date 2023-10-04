using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.Rendering.DebugUI;

// IMPORTANT: Script has been depricated to allow the use of A* instead:
// More recent script is called EnemyAI

public enum EnemyState { PATROLING, CHASINGPLAYER, CHASINGLASTSEEN }

public class EnemyMove : MonoBehaviour
{
    Transform tf;

    public GameObject player;

    public EnemyState state;

    public LayerMask playerAndEnemy;
    LayerMask noEnemy;

    // 1 = Stationary, 2 = Rotating, 3 = Patroling, 4 = Wandering
    public int enemyType;

    bool triggerPatrolingStateChange;
    bool triggerPatrolingLogic;

    bool moveLegal;
    bool moving;

    float baseSpeed = 80;
    float chaseSpeed = 40;

    int xMove;
    int yMove;

    float maxChaseDist;

    Vector2 homeVector;

    RaycastHit2D hit;
    RaycastHit2D targetHome;

    Vector2 moveVectorRaw;
    Vector2 moveVectorNormalized;

    float moveAngle;

    Vector2 lastSawPlayer;

    [Header("EnemyType 1 Variables")]
    float startingRotation;

    [Header("EnemyType 2 Variables")]
    float randomRotTimer;
    float curRotTimer;
    bool triggerRotTimerReset;
    int randomRotDir;

    [Header ("EnemyType 3 Variables")]
    Transform[] waypoints;
    int waypointIndex;

    [Header("EnemyType 4 Variables")]
    RaycastHit2D leftCast;
    RaycastHit2D frontCast;
    RaycastHit2D rightCast;
    RaycastHit2D leftFrontCast;
    RaycastHit2D rightFrontCast;
    bool leftValid;
    bool frontValid;
    bool rightValid;
    bool leftFrontValid;
    bool rightFrontValid;
    int legalMovesCounter;
    int whichMove;
    float rotateToAngle;

    void Start()
    {
        tf = GetComponent<Transform>();

        player = GameObject.FindGameObjectWithTag("Player");

        state = EnemyState.PATROLING;

        noEnemy = LayerMask.GetMask("Enemy");

        triggerPatrolingStateChange = false;
        triggerPatrolingLogic = true;

        moveLegal = true;
        moving = false;

        maxChaseDist = 10;

        if (enemyType == 1 || enemyType == 2)
        {
            homeVector = new Vector2(tf.position.x, tf.position.y);
            if (enemyType == 1)
            {
                startingRotation = tf.rotation.z;
            }
        }
        else if (enemyType == 3)
        {
            waypointIndex = 0;
        }
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (state == EnemyState.PATROLING && triggerPatrolingStateChange)
            {
                triggerPatrolingStateChange = false;
                triggerPatrolingLogic = false;

                PatrolingStateEnter();
            }
            else if (state == EnemyState.PATROLING && triggerPatrolingLogic)
            {
                if (enemyType == 2)
                {
                    if (triggerRotTimerReset)
                    {
                        triggerRotTimerReset = false;
                        randomRotTimer = Random.Range(2f, 7f);
                        curRotTimer = 0;
                    }
                    else
                    {
                        curRotTimer += Time.deltaTime;
                        if (curRotTimer >= randomRotTimer)
                        {
                            randomRotDir = Random.Range(1, 3);
                            if (randomRotDir == 1)
                            {
                                StartCoroutine(RotateTo(tf.rotation.z - 90, 3f));
                            }
                            else
                            {
                                StartCoroutine(RotateTo(tf.rotation.z + 90, 3f));
                            }
                            triggerRotTimerReset = true;
                        }
                    }
                }
                else if (enemyType == 3)
                {
                    if (new Vector2(tf.position.x, tf.position.y) == new Vector2(waypoints[waypointIndex].position.x, waypoints[waypointIndex].position.y))
                    {
                        waypointIndex++;
                        if (waypointIndex > waypoints.Length)
                        {
                            waypointIndex = 0;
                        }
                        StartCoroutine(GoToCoord(waypoints[waypointIndex].position.x, waypoints[waypointIndex].position.y));
                    }
                }
                else if (enemyType == 4)
                {
                    if (!moving)
                    {
                        // Determining which moves are legal
                        legalMovesCounter = 0;

                        leftCast = Physics2D.Raycast(new Vector2(tf.position.x, tf.position.y), Vector2.left, 1, ~playerAndEnemy);
                        frontCast = Physics2D.Raycast(new Vector2(tf.position.x, tf.position.y), Vector2.up, 1, ~playerAndEnemy);
                        rightCast = Physics2D.Raycast(new Vector2(tf.position.x, tf.position.y), Vector2.right, 1, ~playerAndEnemy);
                    
                        if (leftCast.collider == null)
                        {
                            leftValid = true;
                            legalMovesCounter++;
                        }
                        else
                        {
                            leftValid = false;
                        }

                        if (frontCast.collider == null)
                        {
                            frontValid = true;
                            legalMovesCounter++;
                        }
                        else
                        {
                            frontValid = false;
                        }
                    
                        if (rightCast.collider == null)
                        {
                            rightValid = true;
                            legalMovesCounter++;
                        }
                        else
                        {
                            rightValid = false;
                        }

                        if (leftValid && frontValid)
                        {
                            leftFrontCast = Physics2D.Raycast(new Vector2(tf.position.x, tf.position.y), Vector2.left + Vector2.up, 1, ~playerAndEnemy);

                            if (leftFrontCast.collider == null)
                            {
                                leftFrontValid = true;
                                legalMovesCounter++;
                            }
                            else
                            {
                                leftFrontValid = false;
                            }
                        }
                        else
                        {
                            leftFrontValid = false;
                        }

                        if (rightValid && frontValid)
                        {
                            rightFrontCast = Physics2D.Raycast(new Vector2(tf.position.x, tf.position.y), Vector2.right + Vector2.up, 1, ~playerAndEnemy);

                            if (rightFrontCast.collider == null)
                            {
                                rightFrontValid = true;
                                legalMovesCounter++;
                            }
                            else
                            {
                                rightFrontValid = false;
                            }
                        }
                        else
                        {
                            rightFrontValid = false;
                        }
                    
                        // Picking a random move to do

                        /*
                        Random Logic Notes:
                        Left:
                            if whichMove = 0
                        LeftFront:
                            if whichMove = 1
                            if whichMove = 0 && !Left
                        Front:
                            if whichMove = 2
                            if whichMove = 1 || 0 && !LeftFront
                        RightFront:
                            if whichMove = 3
                            if whichMove = 2 || 1 || 0 && !Front
                        Right:
                            if !RightFront
                        */ 
                        if (legalMovesCounter > 0)
                        {
                            whichMove = Random.Range(0, legalMovesCounter);

                            if (whichMove == 0)
                            {
                                if (leftValid)
                                {
                                    rotateToAngle = tf.rotation.z + 90;
                                }
                                else if (leftFrontValid)
                                {
                                    rotateToAngle = tf.rotation.z + 45;
                                }
                                else if (frontValid)
                                {
                                    rotateToAngle = tf.rotation.z;
                                }
                                else if (rightFrontValid)
                                {
                                    rotateToAngle = tf.rotation.z - 45;
                                }
                                else
                                {
                                    rotateToAngle = tf.rotation.z - 90;
                                }
                            }
                            else if (whichMove == 1)
                            {
                                if (leftFrontValid)
                                {
                                    rotateToAngle = tf.rotation.z + 45;
                                }
                                else if (frontValid)
                                {
                                    rotateToAngle = tf.rotation.z;
                                }
                                else if (rightFrontValid)
                                {
                                    rotateToAngle = tf.rotation.z - 45;
                                }
                                else
                                {
                                    rotateToAngle = tf.rotation.z - 90;
                                }
                            }
                            else if (whichMove == 2)
                            {
                                if (frontValid)
                                {
                                    rotateToAngle = tf.rotation.z;
                                }
                                else if (rightFrontValid)
                                {
                                    rotateToAngle = tf.rotation.z - 45;
                                }
                                else
                                {
                                    rotateToAngle = tf.rotation.z - 90;
                                }
                            }
                            else if (whichMove == 3)
                            {
                                if (rightFrontValid)
                                {
                                    rotateToAngle = tf.rotation.z - 45;
                                }
                                else
                                {
                                    rotateToAngle = tf.rotation.z - 90;
                                }
                            }
                            else if (whichMove == 4)
                            {
                                // The only valid move is Right
                                rotateToAngle = tf.rotation.z - 90;
                            }
                            else
                            {
                                Debug.LogError(this.gameObject.name + " cannot think of which move to do in EnemyMove under ET 4");
                            }

                        
                        }
                        // Must've hit a dead end, turn around and go back
                        else if (legalMovesCounter == 0)
                        {
                            rotateToAngle = tf.rotation.z - 180;
                        }

                        if (rotateToAngle != transform.rotation.z)
                        {
                            StartCoroutine(RotateTo(rotateToAngle, baseSpeed));
                        }
                        StartCoroutine(MoveForward());

                        ResetETFourVars();
                    }
                }
                else if (enemyType != 1)
                {
                    Debug.LogError(this.gameObject.name + " is in state: PATROLING, and has no idea what to do");
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
                    lastSawPlayer = new Vector2(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y));
                    state = EnemyState.CHASINGLASTSEEN;
                }
            }
            else if (state == EnemyState.CHASINGLASTSEEN)
            {
                StartCoroutine(GoToCoord(lastSawPlayer.x, lastSawPlayer.y));
            }
        }
    }

    void PatrolingStateEnter()
    {
        // Stationary and Rotating (they do mostly the same thing)
        if (enemyType == 1 || enemyType == 2)
        {
            StartCoroutine(GoToCoord(homeVector.x, homeVector.y));
        }
        // Patroling
        else if (enemyType == 3)
        {
            // Go to the next point on their patrol route
            StartCoroutine(GoToCoord(waypoints[waypointIndex].position.x, waypoints[waypointIndex].position.y));
        }
        // Note: enemyType 4 doesn't need any starting logic, so just make sure it isn't mistaken for no type
        else if (enemyType != 4)
        {
            Debug.LogError(this.name + " does not have it's enemyType set");
        }
    }

    // NOT FINISHED
    IEnumerator RotateTo(float finalRot, float rotSpeed)
    {
        yield return null;
    }

    IEnumerator GoToCoord(float homeX, float homeY)
    {
        moving = true;

        while (tf.position.x != homeX || tf.position.y != homeY)
        {
            CalculateMoveDir(homeX, homeY);

            for (int i = 0; i <= baseSpeed; i++)
            {
                if (xMove == 1)
                {
                    tf.position = new Vector2(tf.position.x + (1 / baseSpeed), tf.position.y);
                }
                else if (xMove == -1)
                {

                    tf.position = new Vector2(tf.position.x - (1 / baseSpeed), tf.position.y);
                }

                if (yMove == 1)
                {
                    tf.position = new Vector2(tf.position.x, tf.position.y + (1 / baseSpeed));
                }
                else if (yMove == -1)
                {
                    tf.position = new Vector2(tf.position.x, tf.position.y - (1 / baseSpeed));
                }

                yield return null;
            }

            tf.position = new Vector2(Mathf.RoundToInt(tf.position.x), Mathf.RoundToInt(tf.position.y));
        }

        if (enemyType == 1 && tf.rotation.z != startingRotation)
        {
            StartCoroutine(RotateTo(startingRotation, 5f));
        }

        triggerPatrolingLogic = true;

        ResetVars();

        moving = false;

        //Debug.Log("Go home coroutine finished");
    }

    IEnumerator EnemyChaseMove(float playerX, float playerY)
    {
        moving = true;

        //Debug.Log("EnemyChaseMove called");

        CalculateMoveDir(playerX, playerY);
        CheckIfHomeInSightForNextMove(tf.position.x + xMove, tf.position.y + yMove, xMove, yMove);

        if (moveLegal)
        {
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
        }

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

    void CheckIfHomeInSightForNextMove(float nextMoveX, float nextMoveY, float homeX, float homeY)
    {
        targetHome = Physics2D.Raycast(new Vector2(nextMoveX, nextMoveY), new Vector2 (homeX, homeY), Mathf.Infinity, ~playerAndEnemy);

        if (targetHome.collider != null)
        {
            //Debug.Log(this.gameObject.name + " has attempted an illegal move to (" + nextMoveX + ", " + nextMoveY + ")");
            moveLegal = false;
        }
    }

    void ResetVars()
    {
        xMove = 0;
        yMove = 0;

        moveLegal = true;
    }

    void ResetETFourVars()
    {
        leftValid = false;
        frontValid = false;
        rightValid = false;
        leftFrontValid = false;
        rightFrontValid = false;

        legalMovesCounter = 0;
        whichMove = 0;
    }

    // This Coroutine takes in no variables, instead, it's based off of the player's rotation.
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

    Rotates counterclockwise
     */
    IEnumerator MoveForward()
    {
        moving = true;

        // xMove
        if (tf.rotation.z >= 22.5 && tf.rotation.z <= 157.5)
        {
            xMove = -1;
        }
        else if (tf.rotation.z >= 202.5 && tf.rotation.z <= 337.5)
        {
            xMove = 1;
        }
        else
        {
            xMove = 0;
        }

        // yMove
        if (tf.rotation.z >= 112.5 && tf.rotation.z <= 247.5)
        {
            yMove = -1;
        }
        else if (tf.rotation.z >= 67.5 && tf.rotation.z <= 292.5)
        {
            yMove = 1;
        }
        else
        {
            yMove = 0;
        }

        if (moveLegal)
        {
            for (int i = 0; i <= baseSpeed; i++)
            {
                if (xMove == 1)
                {
                    tf.position = new Vector2(tf.position.x + (1 / baseSpeed), tf.position.y);
                }
                else if (xMove == -1)
                {

                    tf.position = new Vector2(tf.position.x - (1 / baseSpeed), tf.position.y);
                }

                if (yMove == 1)
                {
                    tf.position = new Vector2(tf.position.x, tf.position.y + (1 / baseSpeed));
                }
                else if (yMove == -1)
                {
                    tf.position = new Vector2(tf.position.x, tf.position.y - (1 / baseSpeed));
                }

                yield return null;
            }

            tf.position = new Vector2(Mathf.RoundToInt(tf.position.x), Mathf.RoundToInt(tf.position.y));
        }

        ResetVars();

        moving = false;
    }
}
