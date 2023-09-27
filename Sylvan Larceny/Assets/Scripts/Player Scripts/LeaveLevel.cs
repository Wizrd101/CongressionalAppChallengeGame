using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml;

// Script goes on the player

public class LeaveLevel : MonoBehaviour
{
    GemCollect gc;
    CoinCounter cc;

    Canvas leavePromptCv;
    TextMeshProUGUI leavePrompt;
    TextMeshProUGUI enemyPrompt;
    TextMeshProUGUI gemPrompt;

    GameObject[] enemies;

    bool notTargeted;
    bool inCircle;
    bool ableToLeave;

    bool textPromptsShow;
    [SerializeField] float textPromptsTimer;

    void Start()
    {
        gc = GetComponent<GemCollect>();
        cc = GetComponent<CoinCounter>();

        leavePromptCv = GameObject.Find("LeavePromptCanvas").GetComponent<Canvas>();
        leavePrompt = leavePromptCv.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        enemyPrompt = leavePromptCv.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        gemPrompt = leavePromptCv.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        leavePromptCv.enabled = false;
        leavePrompt.enabled = false;
        enemyPrompt.enabled = false;
        gemPrompt.enabled = false;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        notTargeted = false;
        inCircle = false;
        ableToLeave = false;

        textPromptsShow = true;

        if (textPromptsTimer == 0)
        {
            textPromptsTimer = 60;
        }
    }

    void Update()
    {
        notTargeted = true;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<EnemyMove>().state != EnemyState.PATROLING)
            {
                notTargeted = false;
            }
        }

        if (inCircle)
        {
            leavePromptCv.enabled = true;

            if (gc.gemCollected && notTargeted)
            {
                ableToLeave = true;

                leavePrompt.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (ableToLeave)
                {
                    cc.SaveCoins();
                    // Leave logic
                }
                else if (textPromptsShow)
                {
                    if (!notTargeted)
                    {
                        StartCoroutine(MoveUpAndFade(enemyPrompt, false));
                    }

                    if (!gc.gemCollected)
                    {
                        StartCoroutine(MoveUpAndFade(gemPrompt, !notTargeted));
                    }
                }
            }
        }
        else
        {
            leavePromptCv.enabled = false;
            leavePrompt.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TeleportCircle")
        {
            inCircle = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "TeleportCircle")
        {
            inCircle = false;
        }
    }

    IEnumerator MoveUpAndFade(TextMeshProUGUI scrollText, bool wait)
    {
        textPromptsShow = false;
        
        if (wait)
        {
            yield return new WaitForSeconds(0.75f);
        }

        Color textColor = new Color(scrollText.color.r, scrollText.color.g, scrollText.color.b, 1);
        float stYValue = 75;
        scrollText.color = textColor;
        scrollText.transform.position = new Vector2(0, stYValue);
        scrollText.enabled = true;

        for (float i = 0; i >= textPromptsTimer; i++)
        {
            textColor.a -= 1 / textPromptsTimer;
            stYValue += 150 / textPromptsTimer;
            scrollText.color = textColor;
            scrollText.transform.position = new Vector2(0, stYValue);
            yield return new WaitForSeconds(0.02f);
        }

        textColor.a = 0;

        scrollText.enabled = false;

        textPromptsShow = true;
    }
}