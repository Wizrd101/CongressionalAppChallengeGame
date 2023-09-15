using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    // Raycast to help detect collision side
    RaycastHit2D leftCheck;
    RaycastHit2D rightCheck;
    RaycastHit2D upCheck;
    RaycastHit2D downCheck;

    // Rock velocity info
    int xDir;
    int yDir;
    float rockVelo;
    Vector2 tempPos;
    Vector2 tempVelo;

    // FixedUpdate is for checking if the rock is still moving
    void FixedUpdate()
    {
        DecreaseVelocity(dropOffCoefficient);

        if (rockMoving && xMoveCheck && yMoveCheck)
        {
            RockStopMoving();
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

    // Method that decreases the velocity by a certain amount
    void DecreaseVelocity(float decreaseAmount)
    {
        if (rb.velocityX > 0)
        {
            rb.velocityX -= decreaseAmount;
            Mathf.Clamp(rb.velocityX, 0, Mathf.Infinity);
        }
        else if (rb.velocityX < 0)
        {
            rb.velocityX += decreaseAmount;
            Mathf.Clamp(rb.velocityX, Mathf.NegativeInfinity, 0);
        }
        else
        {
            xMoveCheck = true;
        }

        if (rb.velocityY > 0)
        {
            rb.velocityY -= decreaseAmount;
            Mathf.Clamp(rb.velocityY, 0, Mathf.Infinity);
        }
        else if (rb.velocityY < 0)
        {
            rb.velocityY += decreaseAmount;
            Mathf.Clamp(rb.velocityY, Mathf.NegativeInfinity, 0);
        }
        else
        {
            yMoveCheck = true;
        }
    }

    // Void that is called when the rock stops moving (to turn it's collider into a trigger,
    void RockStopMoving()
    {
        rockMoving = false;
        bc.isTrigger = true;
    }

    // For while the rock is in motion, so it can bounce off walls, players, and enemies
    void OnCollisionEnter2D(Collision2D other)
    {
        // Aquiring the current position and velocity and storing it in temporary variables
        tempPos = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        tempVelo = new Vector2(rb.velocityX, rb.velocityY);

        // Checking which angle collided with something
        leftCheck = Physics2D.Raycast(tempPos, tempPos + Vector2.left, 1f);
        rightCheck = Physics2D.Raycast(tempPos, tempPos + Vector2.right, 1f);
        upCheck = Physics2D.Raycast(tempPos, tempPos + Vector2.up, 1f);
        downCheck = Physics2D.Raycast(tempPos, tempPos + Vector2.down, 1f);

        // Checks for the proper direction and velocity
        if (leftCheck.collider != null && rb.velocityX < 0 ^ rightCheck.collider != null && rb.velocityX > 0)
        {
            tempVelo.x = -tempVelo.x;
        }

        if (upCheck.collider != null && rb.velocityY > 0 ^ downCheck.collider != null && rb.velocityY < 0)
        {
            tempVelo.y = -tempVelo.y;
        }

        // If the object that the rock collided with can be stunned, stun them
        if (other.gameObject.GetComponent<StunController>() != null)
        {
            StartCoroutine(other.gameObject.GetComponent<StunController>().StunThisGO());
        }

        if (other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            other.gameObject.GetComponent<PlayerHealth>().curHealth--;
        }

        DecreaseVelocity(wallFrictionCoefficient);
    }

    // For after the rock stops moving, so the player can pick it up again
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            tr.rockSupply++;
            Destroy(this.gameObject);
        }
    }
}
