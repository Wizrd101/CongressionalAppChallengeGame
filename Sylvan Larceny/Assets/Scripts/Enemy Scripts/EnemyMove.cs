using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Transform tf;
    Rigidbody2D rb;

    PolygonCollider2D detectCol;

    // 1 = Stationary, 2 = Rotating, 3 = Patroling, 4 = Wandering
    public int enemyType;

    bool chasingPlayer = false;

    // 1 = N, 2 = E, 3 = S, 4 = W
    int faceDir;

    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        detectCol = GetComponentInChildren<PolygonCollider2D>();

        if (tf == null)
        {
            Debug.LogError("EnemyMovement on " + this.name + " script cannot access a Transform");
        }

        if (rb == null)
        {
            Debug.LogError("EnemyMovement on " + this.name + "script cannot access a Rigidbody2D");
        }

        if (detectCol == null)
        {
            Debug.LogError("EnemyMovement on " + this.name + "script cannot access a PolygonCollider2D");
        }
    }

    public void EnemyTurn()
    {
        if (chasingPlayer)
        {
            // Target the player and chase them down
        }
        else
        {
            // Continue on a patrol route
        }
    }
}
