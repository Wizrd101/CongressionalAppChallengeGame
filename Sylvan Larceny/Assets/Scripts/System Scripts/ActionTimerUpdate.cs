using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionTimerUpdate : MonoBehaviour
{
    Slider actionSlider;

    StunController playerSC;
    AdrenalineMode am;

    public float actionTimer;

    bool crippled;

    void Start()
    {
        actionSlider = GameObject.Find("ActionTimerSlider").GetComponent<Slider>();
        
        playerSC = GameObject.Find("Player").GetComponent<StunController>();
        am = GameObject.Find("Player").GetComponent<AdrenalineMode>();

        crippled = false;
    }
    
    void Update()
    {
        if (actionTimer > 0 && !playerSC.stunned)
        {
            actionTimer -= Time.deltaTime;
            Mathf.Clamp(actionTimer, 0f, Mathf.Infinity);
            
            actionSlider.value = actionTimer;
        }
    }

    public IEnumerator CrippleController(float crippleTimer)
    {
        crippled = true;
        yield return new WaitForSeconds(crippleTimer);
        crippled = false;
    }

    public void UpdateTimer(float actionValue)
    {
        Debug.Log("ATU Updated: " + actionValue);
        if (crippled)
        {
            actionValue = actionValue * 2;
        }

        if (!am.inAM)
        {
            actionTimer = actionValue;
            actionSlider.maxValue = actionValue;
        }
    }
}
