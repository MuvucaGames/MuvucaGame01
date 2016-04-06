using UnityEngine;
using System.Collections;
using System;

public class ElementoCarregavel : MonoBehaviour
{
	public bool HeroInn = false;
	public HeroTemp HeroObject;
	public Rigidbody2D ObjectRigidbody;
	public float Altura = 0;
	public float InicialPosition;

	void Start () {
		ObjectRigidbody = GetComponent<Rigidbody2D>();
		foreach (Renderer rg2d in transform.GetComponentsInChildren<Renderer>())
			Altura += rg2d.bounds.size.y;
		InicialPosition = ObjectRigidbody.transform.position.y;
	}

	public void adicionaGravidade(){
		ObjectRigidbody.gravityScale = 2f;
		ObjectRigidbody.isKinematic = false;
		ObjectRigidbody.constraints = RigidbodyConstraints2D.None;
	}
}