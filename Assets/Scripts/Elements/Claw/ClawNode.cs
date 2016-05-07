using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClawNode : Claw {
    public List<Collider2D> itemsOverPlatform = new List<Collider2D>();
    public ClawMechanism clawMechanism = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (heroStrongRigidBody || heroFastRigidBody)
        {
            if (heroStrongRigidBody.Equals(other.attachedRigidbody) ||
                heroFastRigidBody.Equals(other.attachedRigidbody) ||
                other.gameObject.GetComponent<CarriableLight>())
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
