using UnityEngine;
using System.Collections;

public class TriggerForActionableElement : MonoBehaviour {
	public ActionableElement actionableElement;
	public bool triggerOnlyOnce = false;
	private bool alreadyTrigged = false;

	void OnTriggerEnter2D(Collider2D other) {
		if (triggerOnlyOnce && alreadyTrigged) 
		{
			return;
		}

		if (other.tag == "Player") 
		{
			actionableElement.Activate ();
			alreadyTrigged = true;
		}
	}
}
