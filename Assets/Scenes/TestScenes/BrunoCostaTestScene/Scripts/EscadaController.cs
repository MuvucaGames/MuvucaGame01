using UnityEngine;
using System.Collections;
using System;

public class EscadaController : ElementoCarregavel {
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.GetComponentInParent<HeroControlTemp>() != null) {
			HeroObject = coll.GetComponentInParent<HeroTemp>();

			if(!HeroObject._cannCarry){
				ObjectRigidbody.isKinematic = false;
				ObjectRigidbody.constraints = RigidbodyConstraints2D.None;
				HeroInn = true;
				HeroObject.SendMessage("TriggerEscada", this);
			}
		}else if(coll.CompareTag("Floor")){
			ObjectRigidbody.gravityScale = 0;
			ObjectRigidbody.isKinematic = true;
			ObjectRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
		}
	}
	
	void OnTriggerExit2D(Collider2D coll) {
		if (HeroObject != null && !HeroObject._cannCarry) {
			HeroInn = false;
			HeroObject.SendMessage("TriggerEscada", this);
		}
	}

}
