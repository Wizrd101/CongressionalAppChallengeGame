using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Adrenaline Mode is a phase that triggers automatically when an enemy spots you, but (for now)
    can also be triggered manually.

    What happens: The Action timer dissapears, and you can move as much as you want, but cannot
    do other actions. Your movements also get faster. Adrenaline mode is very much limited, so use
    the limited amount wisely.

    Ideas:
        1. Include a cooldown timer for when you trigger AM manually
        2. Make it so that you cannot turn AM off manually as long as you are in sight of an enemy (maybe)
*/

public class AdrenalineMode : MonoBehaviour
{
    PlayerMovement moveScript;

    Slider AMSlider;

    public bool inAM;

    public float maxAdr = 10;
    public float curAdr;

    public float timingVarAlterAmount = 2;
    float baseTimingVar;

    void Start()
    {
        moveScript = GetComponent<PlayerMovement>();

        AMSlider = GameObject.Find("AdrenalineModeSlider").GetComponent<Slider>();

        inAM = false;

        curAdr = maxAdr;

        baseTimingVar = moveScript.timingVar;

        AMSlider.maxValue = maxAdr;
        AMSlider.value = AMSlider.maxValue;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inAM)
            {
                ExitAM();
            }
            else
            {
                EnterAM();
            }
        }

        if (inAM)
        {
            curAdr -= Time.deltaTime;
            Mathf.Clamp(curAdr, 0, maxAdr);
            
            AMSlider.value = curAdr;

            if (curAdr == 0)
            {
                ExitAM();
            }
        }
    }

    public void EnterAM()
    {
        inAM = true;

        moveScript.timingVar = baseTimingVar / timingVarAlterAmount;
    }

    public void ExitAM()
    {
        inAM = false;

        moveScript.timingVar = baseTimingVar;
    }
}
