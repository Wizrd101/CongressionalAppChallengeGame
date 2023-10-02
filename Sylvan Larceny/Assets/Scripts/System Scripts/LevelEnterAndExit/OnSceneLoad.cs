using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneLoad : MonoBehaviour
{
    GameObject player;
    Animator playerAnim;

    public GameObject tpCirclePrefab;
    GameObject tpCircleGO;
    Animator circleAnim;

    [SerializeField] Vector2 playerStartPos;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        Physics2D.queriesHitTriggers = false;

        tpCircleGO = Instantiate(tpCirclePrefab, playerStartPos, Quaternion.identity);

        //player.transform.position = playerStartPos;

        playerAnim = player.GetComponentInChildren<Animator>();
        circleAnim = tpCircleGO.GetComponentInChildren<Animator>();

        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return null;
    }
}
