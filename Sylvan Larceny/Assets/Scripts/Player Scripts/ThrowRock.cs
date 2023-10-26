using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThrowRock : MonoBehaviour
{
    Canvas rtCv;
    Slider throwSlider;
    RockSliderShake sliderShake;

    PlayerMovement pm;

    ActionTimerUpdate atu;

    public TextMeshProUGUI rockText;

    public GameObject rockPrefab;
    GameObject rockSpawn;

    float keyHeldTimer;
    [SerializeField] float keyHeldTimerIncrement;

    public int rockSupply;

    int spawnX;
    int spawnY;

    float throwCharge;
    float chargeIncrement = 0.005f;

    int randomSprite;
    public Sprite[] rockArray; 

    float atuIncrement = 1.2f;

    void Start()
    {
        rtCv = GameObject.Find("RockThrowBarCanvas").GetComponent<Canvas>();
        throwSlider = rtCv.GetComponentInChildren<Slider>();
        sliderShake = throwSlider.GetComponent<RockSliderShake>();

        pm = GetComponent<PlayerMovement>();

        atu = GameObject.Find("ActionTimerController").GetComponent<ActionTimerUpdate>();

        rockText = GameObject.Find("RockText").GetComponent<TextMeshProUGUI>();

        rockSupply = 2;

        rockText.text = "x " + rockSupply;

        rtCv.enabled = false;

        keyHeldTimerIncrement = 1 / 20;
    }

    void Update()
    {
        rtCv.transform.position = this.gameObject.transform.position;

        if (rockSupply > 0)
        {
            if (atu.actionTimer <= 0)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    rtCv.enabled = true;
                    throwCharge += chargeIncrement;
                    throwCharge = 0;
                    keyHeldTimer = 0;
                }

                keyHeldTimer += Time.deltaTime;

                if (Input.GetKey(KeyCode.E) && keyHeldTimer >= keyHeldTimerIncrement)
                {
                    keyHeldTimer = 0;
                    throwCharge += chargeIncrement;
                    throwCharge = Mathf.Clamp01(throwCharge);
                    throwSlider.value = throwCharge;

                    if (throwCharge == 1)
                    {
                        sliderShake.shaking = true;
                    }
                }

                if (Input.GetKeyUp(KeyCode.E))
                {
                    rtCv.enabled = false;
                    sliderShake.shaking = false;
                    rockSupply--;

                    // Figuring out SpawnX and SpawnY
                    if (pm.faceDir == 2 || pm.faceDir == 3 || pm.faceDir == 4)
                    {
                        spawnX = 1;
                    }
                    else if (pm.faceDir == 6 || pm.faceDir == 7 || pm.faceDir == 8)
                    {
                        spawnX = -1;
                    }
                    else
                    {
                        spawnX = 0;
                    }

                    if (pm.faceDir == 1 || pm.faceDir == 2 || pm.faceDir == 8)
                    {
                        spawnY = 1;
                    }
                    else if (pm.faceDir == 4 || pm.faceDir == 5 || pm.faceDir == 6)
                    {
                        spawnY = -1;
                    }
                    else
                    {
                        spawnY = 0;
                    }

                    rockSpawn = Instantiate(rockPrefab, new Vector2(transform.position.x + spawnX, transform.position.y + spawnY), Quaternion.identity);
            
                    RockMove rm = rockSpawn.GetComponent<RockMove>();

                    rm.SetUpProjectile();
                    rm.SetDir(spawnX, spawnY, throwCharge);
                    rm.ActivateProjectile();

                    rm = null;

                    SpriteRenderer sr = rockSpawn.GetComponentInChildren<SpriteRenderer>();
                    if (sr == null)
                    {
                        Debug.Log("ThrowRock couldn't find a SpriteRenderer in rockSpawn's children.");
                    }

                    randomSprite = Random.Range(0, 6);
                    sr.sprite = rockArray[randomSprite];

                    atu.UpdateTimer(throwCharge * atuIncrement);
                    rockText.text = "x " + rockSupply;
                }
            }
        }
    }
}
