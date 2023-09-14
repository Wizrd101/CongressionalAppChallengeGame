using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Transform tf;
    Rigidbody2D rb;

    Slider moveSlider;
    ActionTimerUpdate atu;

    AdrenalineMode AMscript;

    public LayerMask playerAndEnemyMask;

    public float timingVar = 60;

    int xMove;
    int yMove;

    // 1 = N, 2 = NE, 3 = E, 4 = SE, 5 = S, 6 = SW, 7 = W, 8 = NW
    public int faceDir;

    bool moving;

    RaycastHit2D hit;
    bool moveLegal = true;

    // Declaring Variables
    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        AMscript = GetComponent<AdrenalineMode>();

        atu = GameObject.Find("ActionTimerController").GetComponent<ActionTimerUpdate>();

        moving = false;
    }
    
    // Input Detection
    void Update()
    {
        // If the move timer has hit 0, allow the player to input a movement
        if (atu.actionTimer <= 0 && !moving)
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

        // Collision detection, the code won't register an attempt to move into a wall as a move attempt
        // Side to Side
        if (xValue != 0)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(xValue, 0), 1, ~playerAndEnemyMask);
            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.name);
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
                atu.UpdateTimer(2);
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
}
