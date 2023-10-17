using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightsController : MonoBehaviour
{
    GameObject lightsParent;

    Light2D globalLight;
    List<Light2D> torchLights = new List<Light2D>();

    [DoNotSerialize] public bool enteringCaves;

    [SerializeField] bool useInspectorValuesIfZero;
    [SerializeField] float globalLightOffIntensity;
    public float torchLightOnIntensity;

    private void Awake()
    {
        if (!useInspectorValuesIfZero)
        {
            if (globalLightOffIntensity == 0)
                globalLightOffIntensity = 0.1f;
            if (torchLightOnIntensity == 0)
                torchLightOnIntensity = 1f;
        }
    }

    void Start()
    {
        lightsParent = GameObject.Find("LightsGameObjects");

        globalLight = lightsParent.transform.GetChild(0).GetComponent<Light2D>();

        foreach (Light2D light in lightsParent.GetComponentsInChildren<Light2D>())
        {
            if (light != globalLight)
            {
                torchLights.Add(light);
            }
        }

        enteringCaves = false;
    }

    public void FlipLightState()
    {
        if (enteringCaves)
        {
            globalLight.intensity = globalLightOffIntensity;
            
            foreach (Light2D light in torchLights)
            {
                light.intensity = torchLightOnIntensity;
            }
        }
        else
        {
            globalLight.intensity = 1;

            foreach (Light2D light in torchLights)
            {
                light.intensity = 0;
            }
        }

        enteringCaves = !enteringCaves;
    }
}
