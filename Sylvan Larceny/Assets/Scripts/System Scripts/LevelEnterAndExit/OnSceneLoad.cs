using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        Screen.SetResolution(1920, 1080, true);
    }

    void Start()
    {
        Physics2D.queriesHitTriggers = false;

        Time.timeScale = 1.0f;

        //tpCircleGO = Instantiate(tpCirclePrefab, playerStartPos, Quaternion.identity);

        player.transform.position = playerStartPos;

        //playerAnim = player.GetComponentInChildren<Animator>();
        //circleAnim = tpCircleGO.GetComponentInChildren<Animator>();

        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return null;

        if (gameObject.GetComponent<MainMenuLoad>())
        {
            StartCoroutine(gameObject.GetComponent<MainMenuLoad>().MenuLateStart());
        }
        else if (gameObject.GetComponent<LevelLoad>())
        {
            StartCoroutine(gameObject.GetComponent<LevelLoad>().LevelLateStart());
        }
    }
}
