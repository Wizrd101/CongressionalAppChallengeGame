using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Transform tf;
    Rigidbody2D rb;

    Slider moveSlider;

    public float timingVar = 60;

    public float moveTimer = 0;

    int xMove;
    int yMove;

    RaycastHit2D hit;
    bool moveLegal = true;

    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        moveSlider = GameObject.Find("MoveTimerSlider").GetComponent<Slider>();

        if (tf == null)
        {
            Debug.LogError("PlayerMovement script cannot access a Transform");
        }

        if (rb == null)
        {
            Debug.LogError("PlayerMovement script cannot access a Rigidbody2D");
        }

        if (moveSlider == null)
        {
            Debug.LogError("PlayerMovement script cannot access a Move Slider");
        }
    }
    
    // Input Detection
    void Update()
    {
        // If the move timer has hit 0, allow the player to input a movement
        if (moveTimer <= 0)
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

        // Updating the Move Slider to display time between actions
        moveSlider.value = moveTimer;
    }

    IEnumerator GeneralMove(int xValue, int yValue)
    {
        // Update the action timer
        UpdateTimer(2);

        // Collision detection, the code won't register an attempt to move into a wall as a move attempt
        if (xValue != 0)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(xValue, 0), 1);
            if (hit.collider != null)
            {
                moveLegal = false;
            }
        }

        if (yValue != 0)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(0, yValue), 1);
            if (hit.collider != null)
            {
                moveLegal = false;
            }
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

        // Rounds off the position, so the player can move around for forever and still be on tile-based movement
        tf.position = new Vector2(Mathf.RoundToInt(tf.position.x), Mathf.RoundToInt(tf.position.y));
    }

    public void UpdateTimer(float actionValue)
    {
        moveTimer = actionValue;
        moveSlider.maxValue = actionValue;
    }
}
