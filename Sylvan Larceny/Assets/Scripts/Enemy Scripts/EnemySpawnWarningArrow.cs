using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnWarningArrow : MonoBehaviour
{
    [SerializeField] GameObject upPrefab;
    [SerializeField] GameObject downPrefab;
    [SerializeField] GameObject leftPrefab;
    [SerializeField] GameObject rightPrefab;

    GameObject activePrefab;
    int typeOfPrefab;

    Camera cam;

    Vector2 enemyRelPos;

    [SerializeField] Vector2 leftBottom = new Vector2();
    [SerializeField] Vector2 rightTop = new Vector2();

    public float generalWarnDist;
    float xWarnDist;
    float yWarnDist;

    void Start()
    {
        if (!upPrefab || !downPrefab || !leftPrefab || !rightPrefab)
            Debug.LogWarning("Spawn Warning Arrows script on " + this.gameObject.name + " does not have all the prefabs it needs");

        cam = Camera.main.GetComponent<Camera>();

        typeOfPrefab = 0;

        if (generalWarnDist == 0)
            generalWarnDist = 3;
        xWarnDist = generalWarnDist / 16;
        yWarnDist = generalWarnDist / 9;
    }

    void Update()
    {
        enemyRelPos = cam.WorldToViewportPoint(transform.position);
        
        if (!activePrefab)
        {
            if ((enemyRelPos.x < 0 || enemyRelPos.x > 1) ^ (enemyRelPos.y < 0 || enemyRelPos.y > 1))
            {
                // Top
                if (enemyRelPos.y > 1)
                {
                    activePrefab = Instantiate(upPrefab, new Vector3(transform.position.x, rightTop.y, 0), Quaternion.identity);
                    typeOfPrefab = 1;
                }
                // Bottom
                else if (enemyRelPos.y < 0)
                {
                    activePrefab = Instantiate(upPrefab, new Vector3(transform.position.x, leftBottom.y, 0), Quaternion.identity);
                    typeOfPrefab = 2;
                }
                // Right
                else if (enemyRelPos.x > 1)
                {
                    activePrefab = Instantiate(upPrefab, new Vector3(rightTop.x, transform.position.y, 0), Quaternion.identity);
                    typeOfPrefab = 3;
                }
                // Left
                else if (enemyRelPos.x < 0)
                {
                    activePrefab = Instantiate(upPrefab, new Vector3(leftBottom.x, transform.position.y, 0), Quaternion.identity);
                    typeOfPrefab = 4;
                }
                // Logic Error
                else
                {
                    Debug.LogWarning("Spawn Warning arrows script on " + this.gameObject.name + " cannot spawn an arrow correctly");
                }
            }
        }
        else
        {

        }
    }
}
