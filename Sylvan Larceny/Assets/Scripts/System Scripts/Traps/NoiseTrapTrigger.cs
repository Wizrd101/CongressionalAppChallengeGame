using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseTrapTrigger : MonoBehaviour
{
    AudioSource m_as;

    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    [SerializeField] float noiseRadius;

    void Start()
    {
        m_as = GetComponent<AudioSource>();

        noiseRadius = 5f;
    }

    public void NoiseTrapTriggered()
    {
        m_as.Play();

        foreach (GameObject enemy in enemies)
        {
            Vector2 enemyToTrap = new Vector2(enemy.transform.position.x - transform.position.x, enemy.transform.position.y - transform.position.y);
            if (enemyToTrap.magnitude <= noiseRadius)
            {
                // Tell the enemy to go to where the trap was
            }
        }

        Destroy(this.gameObject);
    }
}
