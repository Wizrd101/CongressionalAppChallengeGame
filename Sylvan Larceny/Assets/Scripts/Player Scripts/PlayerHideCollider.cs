using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerHideCollider : MonoBehaviour
{
    PlayerHideController phc;

    public bool thisColHide;

    void Start()
    {
        phc = GetComponentInParent<PlayerHideController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TilemapCollider2D>() == phc.mapHideCol)
        {
            thisColHide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TilemapCollider2D>() == phc.mapHideCol)
        {
            thisColHide = false;
        }
    }
}
