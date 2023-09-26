using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class StunController : MonoBehaviour
{
    PlayerMovement pm;
    ThrowRock ptr;
    AdrenalineMode am;

    EnemyMove em;
    EnemyDetectPlayer edp;

    Animator anim;

    Image atsFill;

    [SerializeField] Color baseColor;
    [SerializeField] Color stunColor;

    float playerStunTime;
    float enemyStunTime;

    bool isPlayer;

    public bool stunned;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        playerStunTime = 1f;
        enemyStunTime = 1.5f;

        if (this.gameObject.tag == "Player")
        {
            pm = GetComponent<PlayerMovement>();
            ptr = GetComponent<ThrowRock>();
            am = GetComponent<AdrenalineMode>();
            atsFill = GameObject.Find("ActionTimerSlider").transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
            isPlayer = true;
        }
        else
        {
            em = GetComponent<EnemyMove>();
            edp = GetComponentInChildren<EnemyDetectPlayer>();
            
            isPlayer = false;
        }

        baseColor = new Color(83/255f, 221/255f, 51/255f);
        stunColor = new Color(26/255f, 68/255f, 17/255f);

        stunned = false;
    }

    public IEnumerator StunThisGO()
    {
        Debug.Log("Stunned: " + this.gameObject.name);

        stunned = true;

        if (isPlayer)
        {
            Debug.Log("Hit Player");
            if (!am.inAM)
            {
                pm.enabled = false;
                ptr.enabled = false;
                atsFill.color = stunColor;
                yield return new WaitForSeconds(playerStunTime);
                pm.enabled = true;
                ptr.enabled = true;
                atsFill.color = baseColor;
            }
        }
        else
        {
            em.enabled = false;
            edp.enabled = false;
            yield return new WaitForSeconds(enemyStunTime);
            em.enabled = true;
            edp.enabled = true;
        }

        stunned = false;
    }
}
