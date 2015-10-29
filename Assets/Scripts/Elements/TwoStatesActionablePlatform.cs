using UnityEngine;
using System.Collections;

public class TwoStatesActionablePlatform : ActionableElement {


	private Vector3 pointA;
	private Vector3 pointB;
 	[SerializeField] private Transform transformB = null;

	[Range(0.0f, 20.0f)]public float speed = 8f;
	[Range(0.00001f, 5f)] public float accelerationDistance = 1.5f;
	[Range(0.00001f, 5f)] public float decelerationDistance = 1.5f;


	private Rigidbody2D rigidBody2D;

	private Coroutine moveCoroutine;
	// Use this for initialization
	void Awake () {
		pointA = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		pointB = transformB.position;

		rigidBody2D = GetComponent<Rigidbody2D> ();
	}


	public override void Activate ()
	{
		if(moveCoroutine!=null)
			StopCoroutine (moveCoroutine);
		moveCoroutine = StartCoroutine (Movement(pointB, pointA) );
	}

	public override void Deactivate ()
	{
		if(moveCoroutine!=null)
			StopCoroutine (moveCoroutine);
		moveCoroutine = StartCoroutine (Movement (pointA, pointB));
	}

	IEnumerator Movement(Vector3 target, Vector3 from){
		float distanceToTarget = Vector3.Distance (transform.position, target);
		while(distanceToTarget > 0.00005f)
		{
			//recalculate the distance between the element's position to the target's position
			distanceToTarget = Vector3.Distance (transform.position, target);
			//recalculate the distance between the element's position and the departure point
			float distanceFromDeparture = Vector3.Distance(transform.position, from);

			//Create a vector that point to target
			Vector3 vel = (target - from);
			//Normalize the vector
			vel.Normalize ();
			//assign the desired speed
			vel *= speed;

			//Correct the speed in case of acceleration or decceleration

			//ACCELERATE
			if(distanceFromDeparture<accelerationDistance){
				vel *= Mathf.Max(distanceFromDeparture/accelerationDistance, 0.005f );
			}

			//DECCELERATE
			if(distanceToTarget< decelerationDistance){
				vel *= distanceToTarget/decelerationDistance;
			}

			//Set the velocity for this update frame
			rigidBody2D.velocity = vel;

			yield return null;
		}

		//rigidBody2D.position = target;
		rigidBody2D.velocity = Vector3.zero;

	}
}
