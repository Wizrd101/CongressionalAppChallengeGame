using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastComponentClose : MonoBehaviour
{
    public bool ableToClose;

    void Start()
    {
        ableToClose = false;
    }

    public void AbleToClose()
    {
        ableToClose = true;
    }
}
