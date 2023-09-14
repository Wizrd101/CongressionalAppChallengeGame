using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Rock abilities:
        1. If the rock enters an enemy's detection zone, they will be curious and walk towards it
        2. If the rock hits an enemy or player, that entity will be stunned and unable to move for a few seconds
 */

public class RockMove : MonoBehaviour
{
    // Rock Components
    Rigidbody2D rb;
    BoxCollider2D bc;

    // The Player's Rock-Throwing script
    ThrowRock tr;

    // Different Coefficients that are needed for calculations
    float startPowerCoefficient;
    float dropOffCoefficient;
    float wallFrictionCoefficient;

    // Different checks to determine when the rock stops moving
    bool rockMoving;
    bool xMoveCheck;
    bool yMoveCheck;

    // Rock velocity info
    int xDir;
    int yDir;
    float rockVelo;
    Vector2 tempVelo;

    // FixedUpdate is for checking if the rock is still moving, and decreasing it's velocity if it is (so it can eventually stop moving)
    void FixedUpdate()
    {
        if (rockMoving)
        {
            if (rb.velocityX > 0)
            {
                rb.velocityX -= dropOffCoefficient;
                Mathf.Clamp(rb.velocityX, 0, Mathf.Infinity);
            }
            else if (rb.velocityX < 0)
            {
                rb.velocityX += dropOffCoefficient;
                Mathf.Clamp(rb.velocityX, Mathf.NegativeInfinity, 0);
            }
            else
            {
                xMoveCheck = true;
            }

            if (rb.velocityY > 0)
            {
                rb.velocityY -= dropOffCoefficient;
                Mathf.Clamp(rb.velocityY, 0, Mathf.Infinity);
            }
            else if (rb.velocityY < 0)
            {
                rb.velocityY += dropOffCoefficient;
                Mathf.Clamp(rb.velocityY, Mathf.NegativeInfinity, 0);
            }
            else
            {
                yMoveCheck = true;
            }
            
            if (xMoveCheck && yMoveCheck)
            {
                RockStopMoving();
            }
        }
    }

    // Void called by ThrowRock. Declares a bunch of variables that I can't declare in Start (as I found out lol)
    public void SetUpProjectile()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        startPowerCoefficient = 3;
        dropOffCoefficient = 0.1f;
        wallFrictionCoefficient = 2;

        rockMoving = true;
        xMoveCheck = false;
        yMoveCheck = false;
        
        tempVelo = Vector2.zero;
    }

    // Sets the movement direction and power that the rock will be thrown
    public void SetDir(int xMove, int yMove, float power)
    {
        xDir = xMove;
        yDir = yMove;
        rockVelo = power * startPowerCoefficient;
    }

    // Actually initializes the movement of the rock
    public void ActivateProjectile()
    {
        rb.velocity = new Vector2(xDir * rockVelo, yDir * rockVelo);
    }

    // Void that is called when the rock stops moving (to turn it's collider into a trigger,
    void RockStopMoving()
    {
        rockMoving = false;
        bc.isTrigger = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        tempVelo = new Vector2(rb.velocityX, rb.velocityY);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            tr.rockSupply++;
            Destroy(this.gameObject);
        }
    }
}
