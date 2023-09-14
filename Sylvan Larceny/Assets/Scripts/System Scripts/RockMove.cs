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
    public Rigidbody2D rb;

    ThrowRock tr;

    [SerializeField] float powerCoefficient;
    [SerializeField] float wallFrictionCoefficient;

    bool rockMoving;

    int xDir;
    int yDir;
    float rockVelo;
    Vector2 tempVelo;

    void Update()
    {
        
    }

    public void SetUpProjectile()
    {
        rb = GetComponent<Rigidbody2D>();

        if (powerCoefficient == 0)
        {
            powerCoefficient = 1;
        }

        tempVelo = Vector2.zero;
    }

    public void SetDir(int xMove, int yMove, float power)
    {
        xDir = xMove;
        yDir = yMove;
        rockVelo = power * powerCoefficient;
    }

    public void ActivateProjectile()
    {
        rb.velocity = new Vector2(xDir * rockVelo, yDir * rockVelo);
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
