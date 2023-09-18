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

    public int rockSupply;

    int spawnX;
    int spawnY;

    float throwCharge;
    [SerializeField] float chargeIncrement = 0.01f;

    [SerializeField] float atuIncrement = 3f;

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
    }

    void Update()
    {
        if (rockSupply > 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                rtCv.enabled = true;
                throwCharge = 0;
            }

            if (Input.GetKey(KeyCode.E))
            {
                throwCharge += chargeIncrement;
                Mathf.Clamp01(throwCharge);
                throwSlider.value = throwCharge;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
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

                atu.UpdateTimer(throwCharge * atuIncrement);

                rockText.text = "x " + rockSupply;
            }
        }
    }
}
