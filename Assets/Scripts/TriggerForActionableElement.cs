using UnityEngine;
using System.Collections;

public class TriggerForActionableElement : MonoBehaviour {
	public ActionableElement actionableElement;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			actionableElement.Activate ();
		}
	}
}
