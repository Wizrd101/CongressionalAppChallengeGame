using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThrowRock : MonoBehaviour
{
    Canvas rtCv;
    Slider throwSlider;

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

    float atuIncrement = 1.2f;

    void Awake()
    {
        rtCv = GameObject.Find("RockThrowBarCanvas").GetComponent<Canvas>();
        throwSlider = rtCv.GetComponentInChildren<Slider>();

        pm = GetComponent<PlayerMovement>();

        atu = GameObject.Find("ActionTimerController").GetComponent<ActionTimerUpdate>();

        rockText = GameObject.Find("RockText").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("E pressed");
                rtCv.enabled = true;
                throwCharge += chargeIncrement;
                throwCharge = 0;
                keyHeldTimer = 0;
            }

            keyHeldTimer += Time.deltaTime;

            if (Input.GetKey(KeyCode.E) && keyHeldTimer >= keyHeldTimerIncrement)
            {
                //Debug.Log("E held");
                keyHeldTimer = 0;
                throwCharge += chargeIncrement;
                throwCharge = Mathf.Clamp01(throwCharge);
                throwSlider.value = throwCharge;
                //Debug.Log(throwCharge);
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                //Debug.Log("E released");
                rtCv.enabled = false;
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
                if (rm == null)
                {
                    Debug.LogError("ThrowRock script on player cannot access RockMove on a rock prefab: " + rockSpawn.gameObject.name);
                }

                rm.SetUpProjectile();
                rm.SetDir(spawnX, spawnY, throwCharge);
                rm.ActivateProjectile();

                rm = null;

                //Debug.Log(throwCharge);
                //Debug.Log(throwCharge * atuIncrement);
                atu.UpdateTimer(throwCharge * atuIncrement);
                rockText.text = "x " + rockSupply;
            }
        }
    }
}
