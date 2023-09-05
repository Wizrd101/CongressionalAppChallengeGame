using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform tf;
    Rigidbody2D rb;

    public float timingVar = 60;

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
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(MoveN());
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(MoveNE());
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(MoveE());
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(MoveSE());
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(MoveS());
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(MoveSW());
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(MoveW());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(MoveNW());
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
