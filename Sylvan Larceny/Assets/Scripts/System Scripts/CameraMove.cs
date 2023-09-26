using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Camera cam;
    Transform camTf;
    
    Transform playerTf;

    Vector3 playerRelPos;

    void Start()
    {
        cam = GetComponent<Camera>();
        camTf = GetComponent<Transform>();

        playerTf = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        playerRelPos = cam.ScreenToWorldPoint(new Vector3(playerTf.position.x, playerTf.position.y, 0));
    }

    IEnumerator CameraMovePosition()
    {
        yield return null;
    }
}
