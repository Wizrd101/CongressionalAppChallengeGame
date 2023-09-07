using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Transform tf;
    Rigidbody2D rb;

    Slider moveSlider;

    AdrenalineMode AMscript;

    public LayerMask playerAndEnemyMask;

    public float timingVar = 60;

    public float moveTimer = 0;

    int xMove;
    int yMove;

    bool moving;

    RaycastHit2D hit;
    bool moveLegal = true;

    // Declaring Variables
    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        AMscript = GetComponent<AdrenalineMode>();

        moveSlider = GameObject.Find("MoveTimerSlider").GetComponent<Slider>();

        if (tf == null)
        {
            Debug.LogError("PlayerMovement script cannot access a Transform");
        }

        if (rb == null)
        {
            Debug.LogError("PlayerMovement script cannot access a Rigidbody2D");
        }

        if (AMscript == null)
        {
            Debug.LogError("PlayerMovement script cannot access the AdrenalineMode script");
        }

        if (moveSlider == null)
        {
            Debug.LogError("PlayerMovement script cannot access a Move Slider");
        }

        playerAndEnemyMask = LayerMask.GetMask("Player");
        playerAndEnemyMask = LayerMask.GetMask("Enemy");

        moving = false;
    }
    
    // Input Detection
    void Update()
    {
        // If the move timer has hit 0, allow the player to input a movement
        if (moveTimer <= 0 && !moving)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.D))
                {
                    //Debug.Log("D");
                    xMove++;
                }

                if (Input.GetKey(KeyCode.A))
                {
                    //Debug.Log("A");
                    xMove--;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    //Debug.Log("W");
                    yMove++;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    //Debug.Log("S");
                    yMove--;
                }

                // Sends the move to the GeneralMove Coroutine if both the xMove and yMove are not 0
                if (xMove != 0 || yMove != 0)
                {
                    //Debug.Log(xMove + " " + yMove);
                    StartCoroutine(GeneralMove(xMove, yMove));
                }
            }

            xMove = 0;
            yMove = 0;
        }
        // Otherwise, the movetimer is still active, so decrease it until it hits 0 (and clamp it there)
        else
        {
            moveTimer -= Time.deltaTime;
            Mathf.Clamp(moveTimer, 0f, Mathf.Infinity);
        }

        // Updating the Move Slider to display time between actions, if the player is not in Adrenaline Mode
        moveSlider.value = moveTimer;
    }

    // General Moving function
    IEnumerator GeneralMove(int xValue, int yValue)
    {
        moving = true;

        // Collision detection, the code won't register an attempt to move into a wall as a move attempt
        // Side to Side
        if (xValue != 0)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(xValue, 0), 1, ~playerAndEnemyMask);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.tag);
                Debug.Log("X check failed");
                moveLegal = false;
            }
        }

        // Up and Down
        if (yValue != 0)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(0, yValue), 1, ~playerAndEnemyMask);
            if (hit.collider != null)
            {
                //Debug.Log("Y check failed");
                moveLegal = false;
            }
        }

        // Diagonals
        if (xValue != 0 && yValue != 0)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(xValue, yValue), Mathf.Sqrt(2), ~playerAndEnemyMask);
            if (hit.collider != null)
            {
                //Debug.Log("Diagonal check failed");
                moveLegal = false;
            }
        }

        if (moveLegal)
        {
            // Update the action timer
            if (!AMscript.inAM)
            {
                UpdateTimer(2);
            }

            // The actual movement part
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

        moveLegal = true;

        // Rounds off the position, so the player can move around for forever and still be on tile-based movement
        tf.position = new Vector2(Mathf.RoundToInt(tf.position.x), Mathf.RoundToInt(tf.position.y));

        moving = false;
    }

    public void UpdateTimer(float actionValue)
    {
        moveTimer = actionValue;
        moveSlider.maxValue = actionValue;
    }
}
