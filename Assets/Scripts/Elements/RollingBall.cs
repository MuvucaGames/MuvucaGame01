using UnityEngine;
using System.Collections;

public class RollingBall : ActionableElementBase
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask heroPlatformMask;
    [SerializeField] private LayerMask mapInteractiveObjectsMask;
    [SerializeField] private Vector3 force = new Vector3(-200f, 0f, 0f);
    [SerializeField] private float deltaTimeStopSound = 0.2f;

    private Collider2D coll = null;
    private Rigidbody2D rigidBody = null;
    private bool isSpawned = false;
    private float gravity;
    private float deltaTimeLastSound = -1;

    // Use this for initialization
    void Awake()
    {
        rigidBody = GetComponentInChildren<Rigidbody2D>();
        coll = GetComponentInChildren<Collider2D>();
        gravity = rigidBody.gravityScale;
        rigidBody.gravityScale = 0;
    }

    void FixedUpdate()
    {        
        if (isActivated)
        {
            if (!isSpawned)
            {
                rigidBody.gravityScale = gravity;
                rigidBody.AddForce(force);
                isSpawned = true;
            }
            if (isGrounded())
            {
                
                if (deltaTimeLastSound < 0 || deltaTimeLastSound + Time.deltaTime > deltaTimeStopSound)
                {
                    SoundManager.Instance.SendMessage("PlaySFXBounceBall");
                }
                deltaTimeLastSound = 0;
            }
            else
            {
                deltaTimeLastSound += Time.deltaTime;
            }

        }

    }
    private bool isGrounded()
    {        
        return coll.IsTouchingLayers(whatIsGround.value | heroPlatformMask.value | mapInteractiveObjectsMask.value); 
    }

}
