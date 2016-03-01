using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerForActionableElement : Activator {

	public bool triggerOnlyOnce = false;
    public bool triggersWithBothInside = false;
    private bool alreadyTrigged = false;
    private HashSet<GameObject> playersInside = new HashSet<GameObject>();

	void OnTriggerEnter2D(Collider2D other) {
		if (triggerOnlyOnce && alreadyTrigged) 
		{
			return;
		}

        if (other.transform.parent != null && other.transform.parent.tag == "Player")
        {
            playersInside.Add(other.transform.parent.gameObject);
        }

        if (playersInside.Count == 2 && triggersWithBothInside) {
            ActivateAll();
            alreadyTrigged = true;
        } else if (playersInside.Count > 0 && !triggersWithBothInside)
        {
            ActivateAll();
            alreadyTrigged = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent != null && other.transform.parent.tag == "Player")
        {
            playersInside.Remove(other.transform.parent.gameObject);
        }
    }
}
