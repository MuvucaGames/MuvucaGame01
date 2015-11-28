using UnityEngine;
using System.Collections;

public class TriggerForActionableElement : Activator {

	public bool triggerOnlyOnce = false;
    public bool triggerOnlyWhenBothCharactersAreInside = false;
	private bool alreadyTrigged = false;
    private int playerCount = 0;

	void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            playerCount++;
        }

        if (triggerOnlyOnce && alreadyTrigged) 
		{
			return;
		}

        if (triggerOnlyWhenBothCharactersAreInside)
        {
            if (playerCount == 2)
            {
                ActivateAll();
                alreadyTrigged = true;
            } else
            {
                // wait for another player
            }
        }
        else
        {
            ActivateAll();
            alreadyTrigged = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerCount--;
        }

    }
}
