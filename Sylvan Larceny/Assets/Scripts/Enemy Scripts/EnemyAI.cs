using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAIState { Patroling, ChasingPlayer, ChasingLastSeen}

public class EnemyAI : MonoBehaviour
{
    AIPath enemyAStarScript;

    public EnemyAIState enemyState;

    public GameObject player;

    public LayerMask playerAndEnemyLayerMask;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        enemyState = EnemyAIState.Patroling;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
