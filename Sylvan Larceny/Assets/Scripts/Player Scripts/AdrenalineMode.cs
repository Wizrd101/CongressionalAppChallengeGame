using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
    Adrenaline Mode is a phase that triggers automatically when an enemy spots you, but (for now)
    can also be triggered manually.

    What happens: The Action timer dissapears, and you can move as much as you want, but cannot
    do other actions. Your movements also get faster. Adrenaline mode is very much limited, so use
    the limited amount wisely.

    Ideas:
 (DONE) 1. Include a cooldown timer for when you trigger AM manually
 (DONE) 2. Make it so that you cannot turn AM off manually as long as you are in sight of an enemy
        3. Make Rocks charge ridiculously fast
*/

public class AdrenalineMode : MonoBehaviour
{
    PlayerMovement moveScript;

    Slider AMSlider;

    public bool inAM;

    public List<EnemyMove> enemies = new List<EnemyMove>();
    bool enemyInSight;
    int enemyCounter;

    public float maxAdr = 10;
    public float curAdr;

    public float timingVarAlterAmount = 2;
    float baseTimingVar;

    [SerializeField] float AMTurnOffTimerBase = 0.5f;
    float AMTurnOffTimer;
    bool manualTurnOffAllowed;

    void Start()
    {
        moveScript = GetComponent<PlayerMovement>();

        AMSlider = GameObject.Find("AdrenalineModeSlider").GetComponent<Slider>();

        inAM = false;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.GetComponent<EnemyMove>() != null)
            {
                enemies.Add(enemy.GetComponent<EnemyMove>());
            }
        }

        enemyInSight = false;

        curAdr = maxAdr;

        baseTimingVar = moveScript.timingVar;

        AMSlider.maxValue = maxAdr;
        AMSlider.value = AMSlider.maxValue;

        AMTurnOffTimer = AMTurnOffTimerBase;
    }

    void Update()
    {
        foreach (EnemyMove move in enemies)
        {
            if (move.state == EnemyState.CHASINGPLAYER)
            {
                enemyInSight = true;
                enemyCounter++;
            }
        }

        if (enemyCounter == 0)
        {
            enemyInSight = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!enemyInSight)
            {
                if (inAM)
                {
                    if (manualTurnOffAllowed)
                    {
                        ExitAM();
                    }
                }
                else
                {
                    EnterAM();
                }
            }
        }

        if (inAM && Time.timeScale != 0)
        {
            curAdr -= Time.deltaTime;
            Mathf.Clamp(curAdr, 0, maxAdr);
            
            AMSlider.value = curAdr;

            if (curAdr == 0)
            {
                ExitAM();
            }

            if (AMTurnOffTimer > 0)
            {
                AMTurnOffTimer -= Time.deltaTime;
                if (manualTurnOffAllowed)
                {
                    manualTurnOffAllowed = false;
                }
            }
            else if (!manualTurnOffAllowed)
            {
                manualTurnOffAllowed = true;
            }
        }
    }

    public void EnterAM()
    {
        inAM = true;

        moveScript.timingVar = baseTimingVar / timingVarAlterAmount;

        AMTurnOffTimer = AMTurnOffTimerBase;
    }

    public void ExitAM()
    {
        inAM = false;

        moveScript.timingVar = baseTimingVar;

        AMTurnOffTimer = 0;
    }
}
