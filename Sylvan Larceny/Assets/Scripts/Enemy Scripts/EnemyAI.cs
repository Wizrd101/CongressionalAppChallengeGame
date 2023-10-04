using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum EnemyAIState { Patroling, ChasingPlayer, ChasingLastSeen}

public class EnemyAI : MonoBehaviour
{
    AIPath enemyAStarScript;
    AIDestinationSetter enemySetTargetScript;

    // Enemy Patrol Script Base: 1 = Stationary, 2 = Rotating, 3 = Patroling, 4 = Wandering
    int epsBase;
    int epsCounter;
    EnemyStationary esBase;
    EnemyRotating erBase;
    EnemyPatrol epBase;
    EnemyWander ewBase;

    Animator anim;

    public GameObject player;
    GameObject lastSawPlayerPoint;

    public EnemyAIState enemyState;
    EnemyAIState oldEnemyState;

    public LayerMask playerAndEnemyLayerMask;

    [SerializeField] float baseSpeed;
    [SerializeField] float chaseSpeed;

    void Start()
    {
        enemyAStarScript = GetComponent<AIPath>();
        enemySetTargetScript = GetComponent<AIDestinationSetter>();

        anim = GetComponentInChildren<Animator>();

        esBase = GetComponent<EnemyStationary>();
        erBase = GetComponent<EnemyRotating>();
        epBase = GetComponent<EnemyPatrol>();
        ewBase = GetComponent<EnemyWander>();

        if (esBase != null)
            epsCounter++;
        if (erBase != null)
            epsCounter++;
        if (epBase != null)
            epsCounter++;
        if (ewBase != null)
            epsCounter++;
        if (epsCounter == 0)
        {
            Debug.LogError("Enemy: " + gameObject.name + " doesn't have a Patrol State script");
        }
        else if (epsCounter >= 2)
        {
            Debug.LogError("Enemy: " + gameObject.name + " has two or more Patrol State scripts");
            esBase = null;
            erBase = null;
            epBase = null;
            ewBase = null;
        }
        else
        {
            if (esBase != null)
                epsBase = 1;
            else if (erBase != null)
                epsBase = 2;
            else if (epBase != null)
                epsBase = 3;
            else if (ewBase != null)
                epsBase = 4;
        }

        player = GameObject.FindWithTag("Player");
        lastSawPlayerPoint = transform.GetChild(3).gameObject;

        enemyState = EnemyAIState.Patroling;

        if (baseSpeed == 0)
            baseSpeed = 0.5f;
        if (chaseSpeed == 0)
            chaseSpeed = 1f;
        enemyAStarScript.maxSpeed = baseSpeed;
    }

    void Update()
    {
        if (oldEnemyState != enemyState)
        {
            if (enemyState == EnemyAIState.Patroling)
            {
                enemyAStarScript.maxSpeed = baseSpeed;
                if (epsBase == 1)
                    esBase.enabled = true;
                else if (epsBase == 2) 
                    erBase.enabled = true;
                else if (epsBase == 3)
                    epBase.enabled = true;
                else if (epsBase == 4)
                    ewBase.enabled = true;
            }
            else if (enemyState == EnemyAIState.ChasingPlayer)
            {
                if (epsBase == 1)
                    esBase.enabled = false;
                else if (epsBase == 2)
                    erBase.enabled = false;
                else if (epsBase == 3)
                    epBase.enabled = false;
                else if (epsBase == 4)
                    ewBase.enabled = false;
                enemyAStarScript.maxSpeed = chaseSpeed;
            }
            else
            {
                lastSawPlayerPoint.transform.position = player.transform.position;
            }
        }

        if (enemyState == EnemyAIState.ChasingPlayer)
        {
            enemySetTargetScript.target = player.transform;
        }
        else if (enemyState == EnemyAIState.ChasingLastSeen)
        {
            enemySetTargetScript.target = lastSawPlayerPoint.transform;
        }
    }

    void LateUpdate()
    {
        oldEnemyState = enemyState;
    }
}
