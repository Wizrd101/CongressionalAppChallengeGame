using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Camera cam;
    Transform camTf;

    Transform playerTf;

    bool canCamMove;

    [SerializeField] Vector2 playerRelPos;

    [SerializeField] float edgeDetectConstant;

    [SerializeField] float cameraMoveTimer;

    [SerializeField] bool timePaused;

    void Start()
    {
        cam = GetComponent<Camera>();
        camTf = GetComponent<Transform>();

        playerTf = GameObject.FindWithTag("Player").GetComponent<Transform>();

        if (cameraMoveTimer == 0)
        {
            cameraMoveTimer = 60;
        }

        timePaused = false;

        canCamMove = true;
    }

    void Update()
    {
        playerRelPos = cam.WorldToViewportPoint(playerTf.position);

        // Left
        if (playerRelPos.x <= 0)
        {
            StartCoroutine(CameraMovePosition(1));
        }
        // Right
        else if (playerRelPos.x >= 1)
        {
            StartCoroutine(CameraMovePosition(2));
        }
        // Top
        else if (playerRelPos.y >= 1)
        {
            StartCoroutine(CameraMovePosition(3));
        }
        // Bottom
        else if (playerRelPos.y <= 0)
        {
            StartCoroutine(CameraMovePosition(4));
        }
    }

    IEnumerator CameraMovePosition(int camMoveDir)
    {
        if (canCamMove)
        {
            canCamMove = false;

            Debug.Log("Cam move called");
        
            Time.timeScale = 0;
            timePaused = true;

            for (float i = 0; i <= cameraMoveTimer; i++)
            {
                // Left
                if (camMoveDir == 1)
                {
                    camTf.position = new Vector3(camTf.position.x - ((cam.orthographicSize * cam.aspect * 2) - 1) / cameraMoveTimer, camTf.position.y, -10);
                }
                // Right
                else if (camMoveDir == 2)
                {
                    camTf.position = new Vector3(camTf.position.x + ((cam.orthographicSize * cam.aspect * 2) - 1) / cameraMoveTimer, camTf.position.y, -10);
                }
                // Top
                else if (camMoveDir == 3)
                {
                    camTf.position = new Vector3(camTf.position.x, camTf.position.y + ((cam.orthographicSize * 2) - 1) / cameraMoveTimer, -10);
                }
                // Bottom
                else
                {
                    camTf.position = new Vector3(camTf.position.x, camTf.position.y - ((cam.orthographicSize * 2) - 1) / cameraMoveTimer, -10);
                }

                yield return new WaitForSecondsRealtime(1 / cameraMoveTimer);
            }
            
            camTf.position = new Vector3(Mathf.RoundToInt(camTf.position.x), Mathf.RoundToInt(camTf.position.y), -10f);

            timePaused = false;
            Time.timeScale = 1;

            canCamMove = true;

            Debug.Log("Cam move finished");
        }
    }
}
