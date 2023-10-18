using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    int tempLevelEnter;

    GameObject player;
    Animator playerAnim;

    GameObject tpCircle;
    Animator tpCircleAnim;

    void Awake()
    {
        tempLevelEnter = SceneManager.GetActiveScene().buildIndex - 2;

        player = GameObject.FindWithTag("Player");
        tpCircle = GameObject.FindWithTag("TeleportCircle");

        playerAnim = player.GetComponentInChildren<Animator>();
        tpCircleAnim = tpCircle.GetComponentInChildren<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public IEnumerator LevelLateStart()
    {
        yield return null;
    }
}
