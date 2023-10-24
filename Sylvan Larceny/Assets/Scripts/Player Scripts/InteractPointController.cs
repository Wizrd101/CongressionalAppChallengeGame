using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPointController : MonoBehaviour
{
    [SerializeField] Transform pointTf;

    public bool ableToInteract;


    
    void Start()
    {
        pointTf = GetComponent<Transform>();

        ableToInteract = true;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<InteractPointReciever>() != null)
            ableToInteract = true;
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
