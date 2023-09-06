using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform tf;
    Rigidbody2D rb;

    public float timingVar = 60;

    public float moveTimer = 0;

    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        if (tf == null)
        {
            Debug.LogError("PlayerMovement script cannot access a Transform");
        }

        if (rb == null)
        {
            Debug.LogError("PlayerMovement script cannot access a Rigidbody2D");
        }
    }
    
    // Input Detection
    void Update()
    {
        // If the move timer has hit 0, allow the player to input a movement
        if (moveTimer <= 0)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    StartCoroutine(MoveNW());
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    StartCoroutine(MoveNE());
                }
                else
                {
                    StartCoroutine(MoveN());
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    StartCoroutine(MoveSW());
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    StartCoroutine(MoveSE());
                }
                else
                {
                    StartCoroutine(MoveS());
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    StartCoroutine(MoveNE());
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    StartCoroutine(MoveSE());
                }
                else
                {
                    StartCoroutine(MoveE());
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    StartCoroutine(MoveNW());
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    StartCoroutine(MoveSW());
                }
                else
                {
                    StartCoroutine(MoveW());
                }
            }
        }
        // Otherwise, the movetimer is still active, so decrease it until it hits 0 (and clamp it there)
        else
        {
            moveTimer -= Time.deltaTime;
            Mathf.Clamp(moveTimer, 0f, Mathf.Infinity);
        }
    }

    IEnumerator MoveN()
    {
        for (int i = 0; i <= timingVar; i++)
        {
            tf.position = new Vector2(tf.position.x, tf.position.y + (1/timingVar));
            yield return null;
        }
        tf.position = new Vector2(Mathf.RoundToInt(tf.position.x), Mathf.RoundToInt(tf.position.y));
    }

    IEnumerator MoveNE()
    {
        for (int i = 0; i <= timingVar; i++)
        {
            tf.position = new Vector2(tf.position.x - (1 / timingVar), tf.position.y + (1 / timingVar));
            yield return null;
        }
        tf.position = new Vector2(Mathf.RoundToInt(tf.position.x), Mathf.RoundToInt(tf.position.y));
    }

    IEnumerator MoveE()
    {
        for (int i = 0; i <= timingVar; i++)
        {
            tf.position = new Vector2(tf.position.x - (1 / timingVar), tf.position.y);
            yield return null;
        }
        tf.position = new Vector2(Mathf.RoundToInt(tf.position.x), Mathf.RoundToInt(tf.position.y));
    }

    IEnumerator MoveSE()
    {
        for (int i = 0; i <= timingVar; i++)
        {
            tf.position = new Vector2(tf.position.x - (1 / timingVar), tf.position.y - (1 / timingVar));
            yield return null;
        }
        tf.position = new Vector2(Mathf.RoundToInt(tf.position.x), Mathf.RoundToInt(tf.position.y));
    }

    IEnumerator MoveS()
    {
        for (int i = 0; i <= timingVar; i++)
        {
            tf.position = new Vector2(tf.position.x, tf.position.y - (1 / timingVar));
            yield return null;
        }
        tf.position = new Vector2(Mathf.RoundToInt(tf.position.x), Mathf.RoundToInt(tf.position.y));
    }

    IEnumerator MoveSW()
    {
        for (int i = 0; i <= timingVar; i++)
        {
            tf.position = new Vector2(tf.position.x + (1 / timingVar), tf.position.y - (1 / timingVar));
            yield return null;
        }
        tf.position = new Vector2(Mathf.RoundToInt(tf.position.x), Mathf.RoundToInt(tf.position.y));
    }

    IEnumerator MoveW()
    {
        for (int i = 0; i <= timingVar; i++)
        {
            tf.position = new Vector2(tf.position.x + (1 / timingVar), tf.position.y);
            yield return null;
        }
        tf.position = new Vector2(Mathf.RoundToInt(tf.position.x), Mathf.RoundToInt(tf.position.y));
    }
    
    IEnumerator MoveNW()
    {
        for (int i = 0; i <= timingVar; i++)
        {
            tf.position = new Vector2(tf.position.x + (1 / timingVar), tf.position.y + (1 / timingVar));
            yield return null;
        }
        tf.position = new Vector2(Mathf.RoundToInt(tf.position.x), Mathf.RoundToInt(tf.position.y));
    }
}
