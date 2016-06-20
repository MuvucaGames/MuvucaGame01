using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// Represents the claw itself. When catching an object, claw will close and continue to do so
/// while holding the object. When letting object go, claw will open.
public class ClawPerSe : Controllable {
    public Carriable interactiveObject = null;
    public Vector3 initialPos;
    private Rigidbody2D interObjRigidbody = null;
    private DistanceJoint2D distJoint = null;
    private PlatformEffector2D platform = null;
    private ClawController controller;
    public Collider2D platformCollider = null;
    public Collider2D platformTrigger = null;
    private Collider2D colliders;
    private float originalGravityScale;

    public bool closedClaw = false;
    public bool openedClaw = false;
    private bool disable = false;
    private bool enable = false;
	public Animator animator= null;

    void Start()
    {
        platform = GetComponentInChildren<PlatformEffector2D>();
        platformCollider = platform.GetComponent<BoxCollider2D>();
        platformCollider.enabled = false;
        platformTrigger.enabled = false;
        distJoint = GetComponent<DistanceJoint2D>();
        distJoint.enabled = false;
        distJoint.distance = 0;
        initialPos = transform.position;
        controller = GetComponentInParent<ClawController>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (platformTrigger.IsTouching(other))
        {
            Hero hero;
            if ((hero = other.GetComponentInParent<Hero>()) != null)
            {
                hero.transform.SetParent(transform);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<BoxCollider2D>().IsTouching(other))
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
            if (!distJoint.connectedBody && 
                other.gameObject.GetComponent<CarriableLight>() != null) 
            {
                if (!other.gameObject.GetComponent<Carriable>().isBeingCarried)
                {
                    interactiveObject = other.GetComponent<CarriableLight>();
                    interObjRigidbody = other.attachedRigidbody;
                }
            }             
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Hero hero;
        if ((hero = other.GetComponentInParent<Hero>()) != null)
        {
            hero.transform.SetParent(null);
        }
        GetComponent<Rigidbody2D>().isKinematic = true;
        if (interactiveObject)
        {            
            interactiveObject = null;       
        }

    }

    void Update()
    {
        if (IsActive) 
        {
            if (distJoint.connectedBody != null)
            {
                platformCollider.enabled = true;                
                platformTrigger.enabled = true;
            }

			if (Input.GetButtonDown ("Action")) 
            {
				if (distJoint.enabled) 
                {
                    disable = true;
				}
 
                else if (interactiveObject && 
                         interactiveObject.gameObject.GetComponent<CarriableLight>() != null) 
                {
					if (GetComponent<BoxCollider2D>().IsTouchingLayers (LayerMask.GetMask ("MapInteractiveObjects"))) 
                    {
                        enable = true;
					}
				}
			}
		} 
        else
        {
            platformCollider.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (disable)
        {
            closedClaw = false;
            openedClaw = true;
            animator.SetBool("close", false);
            animator.SetBool("open", true);
            distJoint.enabled = false;
            distJoint.connectedBody.freezeRotation = false;
            distJoint.connectedBody.gravityScale = originalGravityScale;
            distJoint.connectedBody.GetComponent<Carriable>().isBeingCarried = false;
            distJoint.connectedBody = null;
            platformCollider.enabled = false;
            platformTrigger.enabled = false;

            disable = false;
        }

        if (enable)
        {
            openedClaw = false;
            closedClaw = true;
            animator.SetBool("close", true);
            animator.SetBool("open", false);
            distJoint.enabled = true;
            distJoint.connectedBody = interObjRigidbody;
            distJoint.connectedBody.freezeRotation = true;
            originalGravityScale = distJoint.connectedBody.gravityScale;
            distJoint.connectedBody.gravityScale = 0f;
            distJoint.connectedBody.GetComponent<Carriable>().isBeingCarried = true;
            platformCollider.enabled = true;
            platformTrigger.enabled = true;
            

            enable = false;
        }
    }
}
