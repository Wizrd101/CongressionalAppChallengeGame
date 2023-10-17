using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLights : MonoBehaviour
{
    Light2D thisLight;

    LightsController lc;

    Vector2 lightToCameraPos;

    bool lightOnScreen;

    float baseIntensity;
    [SerializeField] float flickerIntensityRange;
    [SerializeField] float flickerTimerMax;

    float flickerTimer;

    void Awake()
    {
        thisLight = GetComponent<Light2D>();

        // Turn the script off if it's a Global light, as those should not flicker
        if (thisLight.lightType == Light2D.LightType.Global)
        {
            thisLight = null;
            enabled = false;
        }

        lc = GameObject.Find("LightsController").GetComponent<LightsController>();

        baseIntensity = thisLight.intensity;

        if (flickerIntensityRange == 0)
            flickerIntensityRange = 0.15f;
        if (flickerTimerMax == 0)
            flickerTimerMax = 0.1f;
    }

    void Start()
    {
        baseIntensity = lc.torchLightOnIntensity;
    }


    void Update()
    {
        lightToCameraPos = Camera.main.WorldToViewportPoint(thisLight.transform.position);

        if (0 < lightToCameraPos.x && lightToCameraPos.x < 1 && 0 < lightToCameraPos.y && lightToCameraPos.y < 1)
            lightOnScreen = true;
        else
            lightOnScreen = false;


        if (lightOnScreen)
        {
            flickerTimer += Time.deltaTime;

            if (flickerTimer >= flickerTimerMax)
            {
                flickerTimer = 0;

                thisLight.intensity = baseIntensity + Random.Range(-flickerIntensityRange, flickerIntensityRange);
            }
        }
        else
        {
            flickerTimer = 0;

            if (!lc.enteringCaves)
                thisLight.intensity = baseIntensity;
        }
    }
}
