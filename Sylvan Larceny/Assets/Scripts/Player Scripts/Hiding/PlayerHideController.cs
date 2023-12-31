using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerHideController : MonoBehaviour
{
    Animator anim;

    PlayerHideCollider[] playerHideCols;
    public TilemapCollider2D mapHideCol;

    int hideColCounter;

    public bool hiding;

    void Start()
    {
        anim = GameObject.FindWithTag("Player").GetComponentInChildren<Animator>();

        playerHideCols = GameObject.FindWithTag("Player").transform.GetChild(1).GetComponentsInChildren<PlayerHideCollider>();
    }

    void Update()
    {
        hideColCounter = 0;

        for (int i = 0; i < playerHideCols.Length; i++)
        {
            if (playerHideCols[i].thisColHide)
            {
                hideColCounter++;
            }
        }

        if (hideColCounter == playerHideCols.Length)
        {
            hiding = true;
        }
        else
        {
            hiding = false;
        }

        anim.SetBool("Hiding", hiding);
    }
}
