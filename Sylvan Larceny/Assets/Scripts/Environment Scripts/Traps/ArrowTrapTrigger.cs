using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum faceDirection { NotSet, Left, Right, Up, Down }

public class ArrowTrapTrigger : MonoBehaviour
{
    [SerializeField] faceDirection direction;

    AudioSource m_as;

    public GameObject arrowPrefab;
    GameObject arrowSpawn;

    Rigidbody2D arrowRb;

    [SerializeField] float velocityCoefficent;

    void Start()
    {
        if (direction == faceDirection.NotSet)
        {
            Debug.LogError("Arrow Trap: " + this.gameObject.name + " does not have a direction set");
        }

        m_as = GetComponent<AudioSource>();
    }

    public void ArrowTrapTriggered()
    {
        m_as.Play();

        if (direction == faceDirection.Left)
        {
            arrowSpawn = Instantiate(arrowSpawn, new Vector2(-1, 0), Quaternion.identity);
            arrowSpawn.transform.Rotate(0, 0, 90);
            arrowRb = arrowSpawn.GetComponent<Rigidbody2D>();
            arrowRb.velocityX = -velocityCoefficent;
        }
        else if (direction == faceDirection.Right)
        {
            arrowSpawn = Instantiate(arrowSpawn, new Vector2(1, 0), Quaternion.identity);
            arrowSpawn.transform.Rotate(0, 0, 90);
            arrowRb = arrowSpawn.GetComponent<Rigidbody2D>();
            arrowRb.velocityX = velocityCoefficent;
        }
        else if (direction == faceDirection.Up)
        {
            arrowSpawn = Instantiate(arrowSpawn, new Vector2(0, 1), Quaternion.identity);
            arrowRb = arrowSpawn.GetComponent<Rigidbody2D>();
            arrowRb.velocityY = velocityCoefficent;
        }
        else if (direction == faceDirection.Down)
        {
            arrowSpawn = Instantiate(arrowSpawn, new Vector2(0, -1), Quaternion.identity);
            arrowSpawn.transform.Rotate(0, 0, 90);
            arrowRb = arrowSpawn.GetComponent<Rigidbody2D>();
            arrowRb.velocityY = -velocityCoefficent;
        }

        arrowSpawn.GetComponent<ArrowBehavior>().OnArrowCreated();

        Destroy(this.gameObject);
    }
}
