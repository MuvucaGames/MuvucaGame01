using UnityEngine;
using System.Collections;

public class ClawMechanism : Claw
{    
    public bool isOnLimit = false;
    public bool isOnRightLimit = false;
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("LeftLimit")) 
        {
            isOnLimit = true;
        } 

        else if (other.CompareTag ("RightLimit")) 
        {
            isOnRightLimit = true;
        } 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag ("LeftLimit")) {
            isOnLimit = false;
        } 

        else if (other.CompareTag ("RightLimit")) {
            isOnRightLimit = false;
        } 
    }
}