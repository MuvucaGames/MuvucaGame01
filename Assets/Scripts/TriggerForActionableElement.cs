using UnityEngine;
using System.Collections;

public class TriggerForActionableElement : MonoBehaviour {
	public ActionableElement actionableElement;

	void OnTriggerEnter2D(Collider2D other) {
		print (other.tag);
		if (other.tag == "Player") {
			actionableElement.Activate ();
		}
	}
}
