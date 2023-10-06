using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseGame : MonoBehaviour
{
    // Don't forget to set the anim name in the coroutine
    Animator playerAnim;
    Animator cvAnim;

    [SerializeField] float loseAnimTimer;

    void Start()
    {
        playerAnim = GetComponentInChildren<Animator>();
        cvAnim = GameObject.Find("LoseCanvas").GetComponent<Animator>();
        
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
        GameObject.Find("PauseCanvas").GetComponent<PauseMenuController>().gamePausable = false;
        if (GameObject.Find("PauseCanvas").GetComponent<PauseMenuController>().gamePaused)
            GameObject.Find("PauseCanvas").GetComponent<PauseMenuController>().UnpauseGame();

        // SET THIS BEFORE IT WORKS
        playerAnim.Play("");
        
        yield return new WaitForSeconds(loseAnimTimer);

        // SET THIS BEFORE IT WORKS
        cvAnim.Play("");

        Time.timeScale = 0;
    }
}
