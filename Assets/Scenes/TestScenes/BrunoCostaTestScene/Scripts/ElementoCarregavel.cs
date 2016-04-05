using UnityEngine;
using System.Collections;
using System;

public class ElementoCarregavel : MonoBehaviour
{
	public bool HeroInn = false;
	public HeroControlTemp HeroObject;
	public Rigidbody2D ObjectRigidbody;
	public float Altura = 0;
	public float InicialPosition;

	void Start () {
		ObjectRigidbody = GetComponent<Rigidbody2D>();
		foreach (Renderer rg2d in transform.GetComponentsInChildren<Renderer>())
			Altura += rg2d.bounds.size.y;
		InicialPosition = ObjectRigidbody.transform.position.y;
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.GetComponentInParent<HeroControlTemp>() != null) {
			ObjectRigidbody.isKinematic = false;
			ObjectRigidbody.constraints = RigidbodyConstraints2D.None;

			HeroObject = coll.GetComponentInParent<HeroControlTemp>();
			HeroInn = true;
			HeroObject.SendMessage("TriggerEscada", this);
		}else if(coll.CompareTag("Floor")){
			ObjectRigidbody.gravityScale = 0;
			ObjectRigidbody.isKinematic = true;
			ObjectRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
		}
	}
	
	void OnTriggerExit2D(Collider2D coll) {
		if (HeroObject != null) {
			HeroInn = false;
			HeroObject.SendMessage("TriggerEscada", this);
		}
	}

	public void adicionaGravidade(){
		ObjectRigidbody.gravityScale = 2f;
	}
}