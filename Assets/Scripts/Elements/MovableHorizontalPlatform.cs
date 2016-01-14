using UnityEngine;
using System.Collections;

public class MovableHorizontalPlatform : MonoBehaviour {
	[SerializeField] private Vector3 pointA = new Vector3(-2,0,0);
	[SerializeField] private Vector3 pointB = new Vector3(2,0,0);
	[SerializeField, Range(0.0f, 20.0f)] private float speed = 2f;
	[SerializeField] private LayerMask heroMask;
	[SerializeField] private LayerMask mapInteractiveObjectsMask;

	Vector3 target;
	Vector3 from;
	float distanceToTargetPrevious=0;
	float direction=1;
	private Rigidbody2D rigidBody2D;
	private Collider2D collider2D;
	private Transform originalTransform;

	void Awake () {
		pointA = transform.TransformPoint(pointA);
		pointB = transform.TransformPoint(pointB);
		
		from = pointA;
		target = pointB;
		distanceToTargetPrevious = Vector3.Distance (transform.position, target);

		rigidBody2D = GetComponent<Rigidbody2D> ();
		collider2D = GetComponent<Collider2D> ();
		originalTransform = transform;
	}

	// Update is called once per frame
	void FixedUpdate () {
		float distanceToTarget = Vector3.Distance (transform.position, target);
		//when the distance increases, means the direction changes
		if (distanceToTarget > distanceToTargetPrevious) {
			direction = -direction;
			if (direction > 0) {
				target = pointB;
				from = pointA;
			} else {
				target = pointA;
				from = pointB;
			}
			distanceToTargetPrevious = Vector3.Distance (transform.position, target);
			   
		} 
		else {
			distanceToTargetPrevious = distanceToTarget;
		}
		
		Vector3 vel = (target - from);
		//Normalize the vector
		vel.Normalize ();
		//assign the desired speed
		vel *= speed;
		rigidBody2D.velocity = vel;

//		if (hasWeight ())
//			rigidBody2D.isKinematic = true;
//		else
//			rigidBody2D.isKinematic = false;

	}
	private bool hasWeight() {
		//TODO better method to check if grounded
		//old way:
		//bool grounded = Physics2D.OverlapCircle (transform.position, 0.2f, whatIsGround.value | heroPlatformMask.value | mapInteractiveObjectsMask.value);
		//new way:
		bool grounded = collider2D.IsTouchingLayers (heroMask.value | mapInteractiveObjectsMask.value);
		return grounded;
	}

}
