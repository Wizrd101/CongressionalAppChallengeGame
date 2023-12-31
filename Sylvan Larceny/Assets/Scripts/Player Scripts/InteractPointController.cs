using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractPointController : MonoBehaviour
{
    [SerializeField] Transform pointTf;

    public InteractPointReciever currentReciever;

    public bool ableToInteract;
    
    void Start()
    {
        pointTf = GetComponent<Transform>();

        ableToInteract = true;
    }

    void Update()
    {
        if (currentReciever)
        {
            Debug.Log("CurrentReciever is active");
            if (!gameObject.GetComponentInParent<PlayerMovement>().moving && !currentReciever.interactionInPlace)
                ableToInteract = true;
            else
                ableToInteract = false;
        }
        else
        {
            ableToInteract = false;
        }

        if (ableToInteract && currentReciever.ableToRecieve)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                currentReciever.RecieveInteractionActivate();
                //ableToInteract = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<InteractPointReciever>())
            currentReciever = other.gameObject.GetComponent<InteractPointReciever>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<InteractPointReciever>() == currentReciever)
            currentReciever = null;
    }

    public void UpdatePointPos(int faceDir)
    {

        if (faceDir == 1)
            pointTf.localPosition = Vector2.up;
        else if (faceDir == 2)
            pointTf.localPosition = Vector2.up + Vector2.right;
        else if (faceDir == 3)
            pointTf.localPosition = Vector2.right;
        else if (faceDir == 4)
            pointTf.localPosition = Vector2.down + Vector2.right;
        else if (faceDir == 5)
            pointTf.localPosition = Vector2.down;
        else if (faceDir == 6)
            pointTf.localPosition = Vector2.down + Vector2.left;
        else if (faceDir == 7)
            pointTf.localPosition = Vector2.left;
        else if (faceDir == 8)
            pointTf.localPosition = Vector2.up + Vector2.left;
    }
}
