using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///   Support for claw that keeps it fixed on some point.
/// </summary>
///   Support for claw that keeps it fixed on some point. Relative to the point wich claw is 
///   fixed, it moves only upwards or downwards.

public class ClawNode : Claw {
    public LinkedList<Hero> itemsOverPlatform = new LinkedList<Hero>();
    public ClawMechanism clawMechanism = null;
    
    void FixedUpdate()
    {
        if (clawMechanism)
        {
            clawMechanism.transform.position = new Vector2(transform.position.x, 
                                                           clawMechanism.transform.position.y);
        }
    }
}
