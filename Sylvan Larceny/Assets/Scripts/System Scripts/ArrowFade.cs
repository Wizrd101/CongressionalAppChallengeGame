using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFade : MonoBehaviour
{
    SpriteRenderer sr;

    GameObject player;
    Camera cam;

    Vector3 camToPlayer;

    RaycastHit2D hit;
    LayerMask ArrowsOnly;

    float normalAlpha;
    [SerializeField] float fadeAlpha;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        player = GameObject.FindWithTag("Player");
        cam = Camera.main.GetComponent<Camera>();

        ArrowsOnly = LayerMask.GetMask("WarningArrow");

        normalAlpha = sr.color.a;

        if (fadeAlpha == 0)
        {
            fadeAlpha = 127;
        }
    }

    void Update()
    {
        camToPlayer = player.transform.position - cam.transform.position;
        hit = Physics2D.Raycast(cam.transform.position, camToPlayer, camToPlayer.magnitude + 0.1f, ~ArrowsOnly);
        
        if (hit.collider.gameObject == gameObject)
            sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, fadeAlpha);
        else
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, normalAlpha);
    }
}
