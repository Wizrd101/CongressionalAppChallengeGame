using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSliderShake : MonoBehaviour
{
    RectTransform sliderTf;

    Vector2 originalPos;

    float shakeTimer;
    [SerializeField] float shakeTimerTrigger;

    public bool shaking;

    float randomX;
    float randomY;

    [SerializeField] float rangeX;
    [SerializeField] float rangeY;

    void Awake()
    {
        sliderTf = GetComponent<RectTransform>();
    }

    void Start()
    {
        originalPos = sliderTf.localPosition;

        shakeTimer = 0;
        if (shakeTimerTrigger == 0)
        {
            shakeTimerTrigger = 0.1f;
        }

        shaking = false;

        randomX = 0;
        randomY = 0;

        if (rangeX == 0)
        {
            rangeX = 0.5f;
        }

        if (rangeY == 0)
        {
            rangeY = 0.5f;
        }
    }

    void Update()
    {
        if (shaking)
        {
            shakeTimer += Time.deltaTime;
            if (shakeTimer >= shakeTimerTrigger)
            {
                shakeTimer = 0;

                randomX = Random.Range(-rangeX, rangeX);
                randomY = Random.Range(-rangeY, rangeY);

                sliderTf.localPosition = new Vector2(originalPos.x + randomX, originalPos.y + randomY);
            }
        }
        else
        {
            sliderTf.localPosition = originalPos;
        }
    }
}
