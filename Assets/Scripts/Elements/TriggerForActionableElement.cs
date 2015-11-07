using UnityEngine;
using System.Collections;

public class TriggerForActionableElement : Activator {

	public bool triggerOnlyOnce = false;
	private bool alreadyTrigged = false;

	void OnTriggerEnter2D(Collider2D other) {
		if (triggerOnlyOnce && alreadyTrigged) 
		{
			return;
		}

		if (other.tag == "Player") 
		{
			ActivateAll();
			alreadyTrigged = true;
		}
	}
}
