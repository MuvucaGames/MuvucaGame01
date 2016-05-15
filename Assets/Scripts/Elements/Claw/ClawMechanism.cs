using UnityEngine;
using System.Collections;

/// <summary>
///   Piece that move between two physics limits.
/// </summary>
public class ClawMechanism : Claw
{    
    public bool isOnLeftLimit = false;
    public bool isOnRightLimit = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Limit limit = other.GetComponent<Limit>();
        if (limit != null) 
        {
            if (limit.isLeftLimit)
            {
                isOnRightLimit= false;
                isOnLeftLimit = true;
            }
            else if (limit.isRightLimit)
            {
                isOnLeftLimit = false;
                isOnRightLimit = true;
            }
        } 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Limit limit = other.GetComponent<Limit>();
        if (limit != null)
        {
            if (limit.isLeftLimit)
            {
                isOnLeftLimit = false;
            }
            else
            {
                isOnRightLimit = false;
            }
        }
    }
}
