using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    ActionTimerUpdate atu;

    Rigidbody2D rb;

    bool interactable;

    [SerializeField] float crippleCoefficent;
    float crippleTimer;

    public void OnArrowCreated()
    {
        atu = GameObject.Find("ActionTimerController").GetComponent<ActionTimerUpdate>();

        rb = GetComponent<Rigidbody2D>();

        interactable = true;

        if (crippleCoefficent <= 0)
        {
            crippleCoefficent = 1;
        }

        crippleTimer = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (interactable)
        {
            interactable = false;

            if (other.gameObject.tag == "Player")
            {
                crippleTimer = rb.velocity.magnitude * crippleCoefficent;
                StartCoroutine(atu.CrippleController(crippleTimer));
            }
            else if (other.gameObject.tag == "Walls")
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
