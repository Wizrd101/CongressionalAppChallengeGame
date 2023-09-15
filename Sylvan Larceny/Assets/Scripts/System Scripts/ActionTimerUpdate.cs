using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionTimerUpdate : MonoBehaviour
{
    Slider actionSlider;

    AdrenalineMode am;

    public float actionTimer;

    void Start()
    {
        actionSlider = GameObject.Find("ActionTimerSlider").GetComponent<Slider>();
        
        am = GameObject.Find("Player").GetComponent<AdrenalineMode>();
    }
    
    void Update()
    {
        if (actionTimer > 0)
        {
            actionTimer -= Time.deltaTime;
            Mathf.Clamp(actionTimer, 0f, Mathf.Infinity);
            
            actionSlider.value = actionTimer;
        }
    }

    public void UpdateTimer(float actionValue)
    {
        if (!am.inAM)
        {
            actionTimer = actionValue;
            actionSlider.maxValue = actionValue;
        }
    }
}
