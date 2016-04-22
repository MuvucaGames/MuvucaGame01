using UnityEngine;
using System.Collections;

public class MoveAtoB : MonoBehaviour {

	public Transform pointA;
	public Transform pointB;
	public float speed;

	void Start() {
		// If a point A is defined, it will use it as a starting point, otherwise it will use gameobject original position
		if (pointA) {
			transform.position = pointA.position;
		}
	}

	void Update() {
		// based on the speed set movement step
		float step = speed * Time.deltaTime;
		// move GameObject from current position (GameObject own position or if defined point A position) to point B 
		transform.position = Vector3.MoveTowards(transform.position, pointB.position, step);
	}
}
