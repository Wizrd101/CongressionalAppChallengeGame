using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneLoad : MonoBehaviour
{
    GameObject player;

    [SerializeField] Vector2 playerStartPos;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        Physics2D.queriesHitTriggers = false;

        player.transform.position = playerStartPos;
    }
}
