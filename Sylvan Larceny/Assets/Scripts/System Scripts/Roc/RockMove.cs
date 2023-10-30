using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;

/*
    Rock abilities:
        1. If the rock enters an enemy's detection zone, they will be curious and walk towards it
        2. If the rock hits an enemy or player, that entity will be stunned and unable to move for a few seconds
 */

public class RockMove : MonoBehaviour
{
    // Rock Components
    public Rigidbody2D rb;
    BoxCollider2D bc;

    // The Player's Rock-Throwing script
    public ThrowRock tr;

    // Different Coefficients that are needed for calculations
    float startPowerCoefficient;
    float dropOffCoefficient;
    public float wallFrictionCoefficient;

    // Different checks to determine when the rock stops moving
    public bool rockMoving;
    bool xMoveCheck;
    bool yMoveCheck;

    // Raycast to help detect collision side
    RaycastHit2D leftCheck;
    RaycastHit2D rightCheck;
    RaycastHit2D upCheck;
    RaycastHit2D downCheck;

    LayerMask rockMask;

    // Rock velocity info
    int xDir;
    int yDir;
    float rockVelo;
    public Vector2 tempPos;
    public Vector2 tempVelo;

    // FixedUpdate is for checking if the rock is still moving
    void FixedUpdate()
    {
        DecreaseVelocity(dropOffCoefficient);

        tempVelo = new Vector2(rb.velocityX, rb.velocityY);

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

        tr = GameObject.FindGameObjectWithTag("Player").GetComponent<ThrowRock>();

        startPowerCoefficient = 8;
        dropOffCoefficient = 0.1f;
        wallFrictionCoefficient = 0.2f;
        
        rockMoving = true;
        xMoveCheck = false;
        yMoveCheck = false;

        rockMask = LayerMask.GetMask("Rock");
        
        tempVelo = Vector2.zero;

        foreach (Transform bounceScript in transform)
        {
            if (bounceScript.gameObject.GetComponent<RockBounce>() != null)
            {
                bounceScript.gameObject.GetComponent<RockBounce>().SetUpBounceScript();
            }
        }
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
    public void DecreaseVelocity(float decreaseAmount)
    {
        if (Mathf.Abs(rb.velocityX) < decreaseAmount)
        {
            rb.velocityX = 0;
            xMoveCheck = true;
        }
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

        if (Mathf.Abs(rb.velocityY) < decreaseAmount)
        {
            rb.velocityY = 0;
            yMoveCheck = true;
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

    // For after the rock stops moving, so the player can pick it up again
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !rockMoving)
        {
            tr.rockSupply++;
            tr.rockText.text = "x " + tr.rockSupply;
            Destroy(this.gameObject);
        }
    }
}
