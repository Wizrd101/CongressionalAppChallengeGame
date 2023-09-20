using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBounce : MonoBehaviour
{
    public Rigidbody2D rockRb;

    public RockMove rockMoveScript;

    void Update()
    {
        if (!rockMoveScript.rockMoving)
        {
            this.enabled = false;
        }
    }

    public void SetUpBounceScript()
    {
        rockRb = this.gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>();

        rockMoveScript = this.gameObject.GetComponentInParent<RockMove>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject.name == "LeftCol" || this.gameObject.name == "RightCol")
        {
            rockRb.velocityX = -rockRb.velocityX;
        }
        else if (this.gameObject.name == "UpCol" || this.gameObject.name == "DownCol")
        {
            rockRb.velocityY = -rockRb.velocityY;
        }
        else
        {
            Debug.LogWarning("Extra RockBounce called");
        }

        if (other.gameObject.GetComponent<StunController>() != null)
        {
            StartCoroutine(other.gameObject.GetComponent<StunController>().StunThisGO());
        }

        rockMoveScript.DecreaseVelocity(rockMoveScript.wallFrictionCoefficient / rockRb.velocity.magnitude);
    }
}
