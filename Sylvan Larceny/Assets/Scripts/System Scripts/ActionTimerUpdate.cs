using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionTimerUpdate : MonoBehaviour
{
    Slider actionSlider;

    public float actionTimer;

    void Start()
    {
        actionSlider = GameObject.Find("ActionTimerSlider").GetComponent<Slider>();
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
        actionTimer = actionValue;
        actionSlider.maxValue = actionValue;
    }
}
