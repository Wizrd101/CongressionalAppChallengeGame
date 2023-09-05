using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering;

public class TurnOrderController : MonoBehaviour
{
    GameObject Player;
    PlayerMovement pm;
    
    public List<GameObject> Enemies = new List<GameObject>();

    GameObject currentTurnGO;

    int enemyListIndex;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        pm = Player.GetComponent<PlayerMovement>();

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemies.Add(enemy);
        }

        currentTurnGO = Player;
    }

    // Call this function at the end of a unit's turn
    // Function updates which unit is currently taking a turn
    public void updateCurrentTurnGO()
    {
        //Debug.Log("TurnOrderController: updateCurrentTurnGo activated");

        if (currentTurnGO == Player)
        {
            pm.enabled = false;
        }

        if (currentTurnGO == Player)
        {
            currentTurnGO = Enemies[enemyListIndex];
        }
        else if (currentTurnGO.tag == "Enemy")
        {
            enemyListIndex++;
            if (enemyListIndex > Enemies.Count)
            {
                enemyListIndex = 0;
                currentTurnGO = Player;
            }
            else
            {
                currentTurnGO = Enemies[enemyListIndex];
            }
        }
        else
        {
            Debug.LogError("TurnOrderController: updateCurrentTurnGO cannot find current turn's object");
        }

        if (currentTurnGO == Player)
        {
            pm.enabled = true;
        }
        else
        {
            currentTurnGO.GetComponent<EnemyMove>().EnemyTurn();
        }
    }
}
