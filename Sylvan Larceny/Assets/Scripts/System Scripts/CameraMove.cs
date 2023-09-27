using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Camera cam;
    Transform camTf;

    Transform playerTf;

    Vector2 playerRelPos;

    [SerializeField] float edgeDetectConstant;

    [SerializeField] float cameraMoveTimer;

    void Start()
    {
        cam = GetComponent<Camera>();
        camTf = GetComponent<Transform>();

        playerTf = GameObject.FindWithTag("Player").GetComponent<Transform>();

        if (cameraMoveTimer == 0)
        {
            cameraMoveTimer = 60;
        }
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
        Time.timeScale = 0;

        for (float i = 0; i >= cameraMoveTimer; i++)
        {
            // Left
            if (camMoveDir == 1)
            {
                camTf.position = new Vector2(camTf.position.x - 1 / cameraMoveTimer, camTf.position.y);
            }
            // Right
            else if (camMoveDir == 2)
            {
                camTf.position = new Vector2(camTf.position.x + 1 / cameraMoveTimer, camTf.position.y);
            }
            // Top
            else if (camMoveDir == 3)
            {
                camTf.position = new Vector2(camTf.position.x, camTf.position.y + 1 / cameraMoveTimer);
            }
            // Bottom
            else
            {
                camTf.position = new Vector2(camTf.position.x, camTf.position.y - 1 / cameraMoveTimer);
            }

            yield return new WaitForSecondsRealtime(0.02f);
        }

        Time.timeScale = 1;
    }
}
