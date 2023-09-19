using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int curHealth;
    public int maxHealth;

    void Awake()
    {
        if (maxHealth == 0)
        {
            maxHealth = 3;
        }

        curHealth = maxHealth;
    }

    void Start()
    {
        // Here, put the health UI markers
    }

    void Update()
    {
        if (curHealth <= 0)
        {
            // Do lose stuff
        }
    }
}
