using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelPlayNext : MonoBehaviour
{
    Animator coinsTextAnim;

    void Start()
    {
        coinsTextAnim = GameObject.Find("CoinsTextParent").GetComponent<Animator>();
    }

    public void StartCoinsTextAnim()
    {
        coinsTextAnim.SetTrigger("Start");
    }
}
