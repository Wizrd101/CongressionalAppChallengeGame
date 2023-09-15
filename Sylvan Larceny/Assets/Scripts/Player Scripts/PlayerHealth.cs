using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int curHealth;
    public int maxHealth;

    void Start()
    {
        if (maxHealth == 0)
        {
            maxHealth = 3;
        }

        curHealth = maxHealth;
    }

    void Update()
    {
        if (curHealth <= 0)
        {
            // Do lose stuff
        }
    }
}
