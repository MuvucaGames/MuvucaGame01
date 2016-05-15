using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///   Support for claw that keeps it fixed on some point.
/// </summary>
///   Support for claw that keeps it fixed on some point. Relative to the point wich claw is 
///   fixed, it moves only upwards or downwards.

public class ClawNode : Claw {
    public List<Collider2D> itemsOverPlatform = new List<Collider2D>();
    public ClawMechanism clawMechanism = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (heroStrongRigidBody || heroFastRigidBody)
        {
            if (heroStrongRigidBody.Equals(other.attachedRigidbody) ||
                heroFastRigidBody.Equals(other.attachedRigidbody) ||
                other.gameObject.GetComponent<CarriableLight>() != null)
            {
                if (!itemsOverPlatform.Contains(other)) {
                    itemsOverPlatform.Add(other);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (itemsOverPlatform.Contains(other))
        {   
            itemsOverPlatform.Remove(other);
            other.attachedRigidbody.gravityScale = 1f;
        }
    }

    void FixedUpdate()
    {
        if (clawMechanism)
        {
            clawMechanism.transform.position = new Vector2(transform.position.x, clawMechanism.transform.position.y);
        }
    }

}
