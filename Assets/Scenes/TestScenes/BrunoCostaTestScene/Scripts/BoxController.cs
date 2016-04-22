using UnityEngine;
using System.Collections;
using System;

public class BoxController : ElementoCarregavel {

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.CompareTag("Player")) {			
			HeroObject = coll.gameObject.GetComponentInParent<HeroTemp>();
			if(!HeroObject._cannCarry){
				HeroInn = true;
				HeroObject.SendMessage("TriggerEscada", this);
			}
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.CompareTag("Player") && !HeroObject._cannCarry) {
			HeroObject = coll.gameObject.GetComponentInParent<HeroTemp>();
			HeroInn = false;
			HeroObject.SendMessage("TriggerEscada", this);
		}
		
	}
	
}