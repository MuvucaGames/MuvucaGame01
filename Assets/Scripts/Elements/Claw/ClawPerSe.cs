using UnityEngine;
using System.Collections;

/// Represents the claw itself. When catching an object, claw will close and continue to do so
/// while holding the object. When letting object go, claw will open.
public class ClawPerSe : Claw {
    public GameObject interactiveObject = null;
    private Rigidbody2D interObjRigidbody = null;
    private HingeJoint2D jointInterObject = null;
	private ClawNode clawNode = null;

    public bool closedClaw = false;
    public bool openedClaw = false;

	public Animator animator= null;

    void Start()
    {
		clawNode = GetComponentInChildren<ClawNode> ();
		jointInterObject = GetComponent<HingeJoint2D> ();
		jointInterObject.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        if (!jointInterObject.connectedBody && other.gameObject.GetComponent<CarriableLight>() != null) 
        {
            interactiveObject = other.gameObject;
            interObjRigidbody = other.attachedRigidbody;
        } 
    }

    void OnTriggerExit2D()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;       
    }

    void Update()
    {
        if (controller.active) {
			if (jointInterObject.connectedBody)
            {
				jointInterObject.connectedBody.isKinematic = false;
            }
			if (Input.GetButtonDown ("Action")) {
				if (jointInterObject.connectedBody) {
					closedClaw = false;
					openedClaw = true;
					animator.SetBool("close", false);
					animator.SetBool("open", true);
					jointInterObject.enabled = false;
					jointInterObject.connectedBody.freezeRotation = false;
					jointInterObject.connectedBody = null;
				} else if (interactiveObject) {
					if (GetComponent<BoxCollider2D> ().IsTouchingLayers (LayerMask.GetMask ("MapInteractiveObjects"))) {
						openedClaw = false;
						closedClaw = true;
						animator.SetBool("close", true);
						animator.SetBool("open", false);
						jointInterObject.enabled = true;
						jointInterObject.connectedBody = interObjRigidbody;
						jointInterObject.connectedBody.freezeRotation = true;
					}
				}
			}
		} else {
			if (jointInterObject.connectedBody)
            {
                GetComponent<Rigidbody2D>().isKinematic = true;
			    jointInterObject.connectedBody.isKinematic = true;
            }                
		}
    }

    void FixedUpdate()
    {
        if (closedClaw)
        {
            interactiveObject.transform.position = transform.position;
        }
    }

	public void changeGravityScale(float gravScale)
	{
		foreach (Collider2D coll in clawNode.itemsOverPlatform) {
			coll.attachedRigidbody.gravityScale = gravScale;
		}
		if (interObjRigidbody)
		interObjRigidbody.gravityScale = gravScale;
	}
}
