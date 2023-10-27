using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePLRC : MonoBehaviour
{
    Animator imageAnim;
    Animator pannelAnim;

    LastComponentClose lcc;

    void Start()
    {
        imageAnim = GetComponent<Animator>();
        pannelAnim = GameObject.Find("PLRCPannelImage").GetComponent<Animator>();

        lcc = GameObject.Find("CoinsTextParent").GetComponent<LastComponentClose>();
    }

    public void OnClickClose()
    {
        if (lcc.ableToClose)
        {
            StartCoroutine(CloseReview());
        }
    }

    IEnumerator CloseReview()
    {
        pannelAnim.SetTrigger("End");
        yield return new WaitForSeconds(0.5f);
        imageAnim.SetTrigger("End");
    }
}
