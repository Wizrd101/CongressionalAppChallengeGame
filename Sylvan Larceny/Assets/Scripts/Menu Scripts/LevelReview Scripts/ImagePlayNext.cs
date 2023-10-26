using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePlayNext : MonoBehaviour
{
    Animator pannelAnim;

    void Start()
    {
        pannelAnim = GameObject.Find("PLRCPannelImage").GetComponent<Animator>();
    }

    public void StartPannelAnim()
    {
        Debug.Log("Play Next");
        pannelAnim.SetTrigger("Start");
    }
}
