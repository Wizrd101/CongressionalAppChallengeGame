using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CaveTeleport : MonoBehaviour
{
    CaveTeleport caveMatch;

    LightsController lc;

    GameObject player;

    Canvas tpCv;

    Animator sceneTransition;

    [DoNotSerialize] public int teleportChannel;

    [SerializeField] Vector2 recievePos;

    bool ableToEnter;

    void Awake()
    {
        teleportChannel = int.Parse(name);

        player = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        tpCv = GameObject.Find("CaveTeleportCanvas").GetComponent<Canvas>();
        
        //lc = GameObject.Find("LightsController").GetComponent<LightsController>();

        sceneTransition = GameObject.Find("LevelLoader").GetComponent<Animator>();

        if (recievePos == Vector2.zero)
            recievePos = new Vector2(transform.position.x, transform.position.y);

        ableToEnter = false;

        foreach (CaveTeleport ct in transform.parent.GetComponentsInChildren<CaveTeleport>())
        {
            if (teleportChannel == ct.teleportChannel)
            {
                caveMatch = ct;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ableToEnter)
        {
            StartCoroutine(CaveTP());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            tpCv.enabled = true;
            ableToEnter = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            tpCv.enabled = false;
            ableToEnter = false;
        }
    }

    IEnumerator CaveTP()
    {
        sceneTransition.SetBool("LoopBack", true);
        sceneTransition.SetTrigger("Start");
        
        yield return new WaitForSeconds(1);

        //lc.FlipLightState();
        player.transform.position = caveMatch.recievePos;
    }
}
