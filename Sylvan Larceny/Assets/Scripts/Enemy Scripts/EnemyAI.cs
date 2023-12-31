using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;

// This script controls the state changes of the 

public enum EnemyAIState { Start, Patroling, ChasingPlayer, ChasingLastSeen}

public class EnemyAI : MonoBehaviour
{
    /*// Enemy Patrol Script Base: 1 = Stationary, 2 = Rotating, 3 = Patroling, 4 = Wandering
    int epsBase;
    int epsCounter;
    EnemyStationary esBase;
    EnemyRotating erBase;
    EnemyPatrol epBase;
    EnemyWander ewBase;

    Animator anim;

    public GameObject player;
    Transform playerLastSeenPoint;

    public EnemyAIState enemyState;
    EnemyAIState oldEnemyState;

    public LayerMask playerAndEnemyLayerMask;

    [SerializeField] float baseSpeed;
    [SerializeField] float chaseSpeed;

    void Start()
    {
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
        playerLastSeenPoint = player.transform;

        oldEnemyState = EnemyAIState.Start;
        enemyState = EnemyAIState.Patroling;

        if (baseSpeed == 0)
            baseSpeed = 0.5f;
        if (chaseSpeed == 0)
            chaseSpeed = 1f;
    }

    void Update()
    {
        if (oldEnemyState != enemyState)
        {
            if (oldEnemyState == EnemyAIState.Start)
            {
                oldEnemyState = EnemyAIState.Start;
            }
            
            if (enemyState == EnemyAIState.Patroling)
            {
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
            }
            // Transitioning to ChasingLastSeen
            else
            {
                playerLastSeenPoint.position = new Vector3 (Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y), 0);
            }
        }
        else
        {
            if (enemyState == EnemyAIState.ChasingLastSeen)
            {
                if (transform.position == playerLastSeenPoint.position)
                {

                }
            }
        }
    }

    void LateUpdate()
    {
        oldEnemyState = enemyState;
    }*/

    AIPath aStarPathfinder;
    AIDestinationSetter aStarDestinationSetter;
    Animator anim;

    public GameObject player;
    Transform playerLastSeenPoint;

    public EnemyAIState enemyState;
    EnemyAIState oldEnemyState;

    Transform homePos;

    RaycastHit2D sightHit;

    public LayerMask enemyLayerMask;

    [SerializeField] float baseSpeed;
    [SerializeField] float chaseSpeed;

    void Start()
    {
        aStarPathfinder = GetComponent<AIPath>();
        aStarDestinationSetter = GetComponent<AIDestinationSetter>();
        anim = GetComponentInChildren<Animator>();
        
        player = GameObject.FindWithTag("Player");
        playerLastSeenPoint = transform.GetChild(2).transform;

        oldEnemyState = EnemyAIState.Patroling;
        enemyState = EnemyAIState.Patroling;

        homePos = transform.GetChild(4).transform;
        homePos.position = new Vector3(Mathf.RoundToInt(homePos.position.x), Mathf.RoundToInt(homePos.position.y), 0);

        enemyLayerMask = LayerMask.GetMask("Enemy");

        if (baseSpeed == 0)
            baseSpeed = 0.5f;
        if (chaseSpeed == 0)
            chaseSpeed = 1f;

        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
    }

    void Update()
    {
        if (enemyState == EnemyAIState.ChasingPlayer)
        {
            Debug.Log("Do raycast");

            sightHit = Physics2D.Linecast(transform.position, player.transform.position, ~enemyLayerMask);

            if (sightHit.collider.tag != "Player")
            {
                Debug.Log("Raycast change state");
                enemyState = EnemyAIState.ChasingLastSeen;
            }
        }
        else if (enemyState == EnemyAIState.ChasingLastSeen)
        {
            if (0.1f <= Vector3.Distance(transform.position, playerLastSeenPoint.position))
            {
                enemyState = EnemyAIState.Patroling;
            }
        }

        if (oldEnemyState != enemyState)
        {
            if (enemyState == EnemyAIState.Patroling)
            {
                playerLastSeenPoint.position = homePos.position;
                aStarPathfinder.maxSpeed = baseSpeed;
                aStarDestinationSetter.target = homePos;
            }
            else if (enemyState == EnemyAIState.ChasingPlayer)
            {
                aStarPathfinder.maxSpeed = chaseSpeed;
                aStarDestinationSetter.target = player.transform;
            }
            else if (enemyState == EnemyAIState.ChasingLastSeen)
            {
                playerLastSeenPoint.position = new Vector3(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y), 0);
                aStarPathfinder.maxSpeed = chaseSpeed;
                aStarDestinationSetter.target = playerLastSeenPoint;
            }
        }

        oldEnemyState = enemyState;
    }

    
}
