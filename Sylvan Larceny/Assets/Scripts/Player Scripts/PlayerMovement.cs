using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Transform tf;

    ActionTimerUpdate atu;

    AdrenalineMode AMscript;

    Camera cam;

    List <Transform> enemies = new List <Transform>();

    InteractPointController ipc;

    public bool playerCanMove;

    Vector2 enemyToPlayer;
    Vector2 closestEnemyToPlayer;
    Vector2 tempEnemyPos;
    float atuTimerUpdate;
    bool enemyOnScreen;
    [SerializeField] float enemyUpdateTimerDist;

    public LayerMask playerAndEnemyMask;

    public float timingVar = 60;
    [SerializeField] float atuTimerCoefficent;

    int xMove;
    int yMove;

    // 1 = N, 2 = NE, 3 = E, 4 = SE, 5 = S, 6 = SW, 7 = W, 8 = NW
    public int faceDir;

    public bool moving;

    RaycastHit2D hit;
    bool moveLegal = true;

    // Declaring Variables
    void Start()
    {
        tf = GetComponent<Transform>();
        AMscript = GetComponent<AdrenalineMode>();

        if (SceneManager.GetActiveScene().name != "MainMenuScene")
            atu = GameObject.Find("ActionTimerController").GetComponent<ActionTimerUpdate>();

        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        foreach (GameObject enemyGO in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyGO.GetComponent<Transform>();
        }

        ipc = GetComponentInChildren<InteractPointController>();

        playerCanMove = true;

        if (enemyUpdateTimerDist == 0)
            enemyUpdateTimerDist = 5;

        atuTimerUpdate = 0;

        if (atuTimerCoefficent == 0)
            atuTimerCoefficent = 4;

        moving = false;
    }
    
    // Input Detection
    void Update()
    {
        // If the move timer has hit 0, allow the player to input a movement
        if (SceneManager.GetActiveScene().name == "MainMenuScene" || (atu.actionTimer <= 0 && !moving && Time.timeScale != 0 && playerCanMove))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.D))
                {
                    xMove++;
                }

                if (Input.GetKey(KeyCode.A))
                {
                    xMove--;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    yMove++;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    yMove--;
                }

                // Figuring out FaceDir (Needed for the ThrowRock script and Animations)
                if (xMove == 1)
                {
                    if (yMove == 1)
                    {
                        faceDir = 2;
                    }
                    else if (yMove == -1)
                    {
                        faceDir = 4;
                    }
                    else
                    {
                        faceDir = 3;
                    }
                }
                else if (xMove == -1)
                {
                    if (yMove == 1)
                    {
                        faceDir = 8;
                    }
                    else if (yMove == -1)
                    {
                        faceDir = 6;
                    }
                    else
                    {
                        faceDir = 7;
                    }
                }
                else
                {
                    if (yMove == 1)
                    {
                        faceDir = 1;
                    }
                    else
                    {
                        faceDir = 5;
                    }
                }

                // Sends the move to the GeneralMove Coroutine if both the xMove and yMove are not 0
                if (xMove != 0 || yMove != 0)
                {
                    StartCoroutine(PlayerMove(xMove, yMove));
                }
            }

            xMove = 0;
            yMove = 0;
        }
    }

    // General Moving function
    IEnumerator PlayerMove(int xValue, int yValue)
    {
        moving = true;
        moveLegal = true;

        // Collision detection, the code won't register an attempt to move into a wall as a move attempt
        // Side to Side
        if (xValue != 0)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(xValue, 0), 1, ~playerAndEnemyMask);
            if (hit.collider != null && hit.collider.name != "InteractPoint")
            {
                moveLegal = false;
            }
        }

        // Up and Down
        if (yValue != 0)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(0, yValue), 1, ~playerAndEnemyMask);
            if (hit.collider != null && hit.collider.name != "InteractPoint")
            {
                moveLegal = false;
            }
        }

        // Diagonals
        if (xValue != 0 && yValue != 0)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(xValue, yValue), Mathf.Sqrt(2), ~playerAndEnemyMask);
            if (hit.collider != null)
            {
                moveLegal = false;
            }
        }

        if (moveLegal)
        {
            // Update the action timer
            if (!AMscript.inAM)
            {
                closestEnemyToPlayer = new Vector2(999, 999);
                enemyOnScreen = false;

                foreach (Transform enemy in enemies)
                {
                    tempEnemyPos = cam.WorldToViewportPoint(enemy.position);

                    if (tempEnemyPos.x < 0.5f)
                    {
                        tempEnemyPos.x -= ((enemy.gameObject.GetComponent<BoxCollider2D>().size.x / 2) / Screen.width);
                    }
                    else
                    {
                        tempEnemyPos.x += ((enemy.gameObject.GetComponent<BoxCollider2D>().size.x / 2) / Screen.width);
                    }

                    if (tempEnemyPos.y < 0.5f)
                    {
                        tempEnemyPos.y -= ((enemy.gameObject.GetComponent<BoxCollider2D>().size.y / 2) / Screen.height);
                    }
                    else
                    {
                        tempEnemyPos.y += ((enemy.gameObject.GetComponent<BoxCollider2D>().size.y / 2) / Screen.height);
                    }

                    if (!enemy.gameObject.GetComponent<StunController>().stunned)
                    {
                        if ((0 < tempEnemyPos.x && tempEnemyPos.x < 1 && 0 < tempEnemyPos.y && tempEnemyPos.y < 1) || enemy.GetComponent<EnemyAIState>() == EnemyAIState.ChasingPlayer)
                        {
                            enemyToPlayer = enemy.position - tf.position;

                            if (enemyToPlayer.magnitude < closestEnemyToPlayer.magnitude)
                            {
                                closestEnemyToPlayer = enemyToPlayer;
                            }
                        }
                        else
                        {
                            enemyOnScreen = true;
                        }
                    }
                }

                if (enemyOnScreen && closestEnemyToPlayer.magnitude <= enemyUpdateTimerDist)
                {
                    atuTimerUpdate = timingVar / 60 + ((closestEnemyToPlayer.magnitude - enemyUpdateTimerDist) / atuTimerCoefficent);
                }
                else
                {
                    atuTimerUpdate = timingVar / 60;
                }

                atu.UpdateTimer(atuTimerUpdate);
            }

            // The actual movement part
            if (Time.timeScale != 0)
            {
                for (int i = 0; i <= timingVar; i++)
                {
                    if (xValue == 1)
                    {
                        tf.position = new Vector2(tf.position.x + (1 / timingVar), tf.position.y);
                    }
                    else if (xValue == -1)
                    {
                        tf.position = new Vector2(tf.position.x - (1 / timingVar), tf.position.y);
                    }

                    if (yValue == 1)
                    {
                        tf.position = new Vector2(tf.position.x, tf.position.y + (1 / timingVar));
                    }
                    else if (yValue == -1)
                    {
                        tf.position = new Vector2(tf.position.x, tf.position.y - (1 / timingVar));
                    }

                    yield return null;
                }
            }
        }
        else
        {
            Debug.Log("Move is not legal");
        }

        // Rounds off the position, so the player can move around for forever and still be on tile-based movement
        tf.position = new Vector2(Mathf.RoundToInt(tf.position.x), Mathf.RoundToInt(tf.position.y));

        ipc.UpdatePointPos(faceDir);

        moving = false;
    }
}
