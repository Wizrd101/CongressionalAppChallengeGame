using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseGame : MonoBehaviour
{
    // Don't forget to set the anim name in the coroutine
    Animator playerAnim;
    Animator levelLoaderAnim;

    [SerializeField] float loseAnimTimer;

    void Start()
    {
        playerAnim = GetComponentInChildren<Animator>();
        levelLoaderAnim = GameObject.Find("LevelLoader").transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
        
        if (loseAnimTimer == 0)
            loseAnimTimer = 1;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(LoseGameSequence());
        }
    }

    IEnumerator LoseGameSequence()
    {
        PlayerPrefs.SetInt("LevelSuccess", 2);

        GameObject.Find("PauseCanvas").GetComponent<PauseMenuController>().gamePausable = false;
        if (GameObject.Find("PauseCanvas").GetComponent<PauseMenuController>().gamePaused)
            GameObject.Find("PauseCanvas").GetComponent<PauseMenuController>().UnpauseGame();

        // SET THIS BEFORE IT WORKS (To the lose anim)
        playerAnim.SetTrigger("");
        
        yield return new WaitForSeconds(loseAnimTimer);

        levelLoaderAnim.SetBool("LoopBack", false);
        levelLoaderAnim.SetTrigger("Start");

        Time.timeScale = 0;
    }
}
