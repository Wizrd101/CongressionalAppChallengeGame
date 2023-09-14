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
    Rigidbody2D rb;

    [SerializeField] float powerCoefficient;
    [SerializeField] float wallFrictionCoefficient;

    int xDir;
    int yDir;
    float rockVelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (powerCoefficient == 0)
        {
            powerCoefficient = 1;
        }
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

    public void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}
