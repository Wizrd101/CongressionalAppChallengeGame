using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    [SerializeField] List<GameObject> uiElements = new List<GameObject>();

    Camera cam;
    GameObject player;

    [SerializeField] Vector3 camToPlayer;

    //RaycastHit2D hit;
    //LayerMask uiOnly;

    [SerializeField] int fadeAlpha;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            uiElements.Add(transform.GetChild(i).gameObject);
        }

        cam = Camera.main.GetComponent<Camera>();
        player = GameObject.FindWithTag("Player");

        //uiOnly = LayerMask.GetMask("UI");

        if (fadeAlpha == 0)
        {
            fadeAlpha = 127;
        }

        /*foreach (GameObject element in uiElements)
        {
            if (element.GetComponent<Image>() != null)
            {
                element.GetComponent<Image>().raycastTarget = true;
            }
            else if (element.GetComponent<TextMeshProUGUI>() != null)
            {
                element.GetComponent<TextMeshProUGUI>().raycastTarget = true;
            }
            else if (element.GetComponent<Slider>() != null)
            {
                
            }
        }*/
    }

    void Update()
    {
        camToPlayer = player.transform.position - cam.transform.position;

        //hit = Physics2D.Raycast(cam.transform.position, camToPlayer, (camToPlayer.magnitude + 0.1f), uiOnly);
    }
}
