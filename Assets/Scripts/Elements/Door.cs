using UnityEngine;
using System.Collections;

public class Door : ActionableElement {
<<<<<<< HEAD
	private Vector3 closedPosition;
	private Vector3 openedPosition;
    // Check door closed or opened: if checked, then its opened, if unchecked, then closed
    public bool isDoorOpened;

    void Awake()
=======
	public Vector3 openedPosition = new Vector3(0,3,0);
	public float maxSpeed = 0.5f;
	public float maxForce = 10f;
	public bool fallWithGravity = true;

	private Rigidbody2D movablePart;
	Coroutine movement;
	void Awake()
>>>>>>> origin/master
	{
		movablePart = GetComponentInChildren<Rigidbody2D>();
	}

	public override void Activate()
	{
		if (movement != null)
			StopCoroutine (movement);
		movement = StartCoroutine (Movement(transform.TransformPoint (openedPosition)));
        SoundManager.Instance.SendMessage("PlaySFXOpenGate");
        isDoorOpened = true;
	}

	public override void Deactivate()
	{
<<<<<<< HEAD
		gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        SoundManager.Instance.SendMessage("PlaySFXCloseGate");
        isDoorOpened = false;
=======

		if (movement != null)
			StopCoroutine (movement);

		if (fallWithGravity) {
			movablePart.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
		} else {
			movement = StartCoroutine(Movement(transform.position));
		}

		SoundManager.Instance.SendMessage("PlaySFXCloseGate");
	}

	private IEnumerator Movement(Vector3 target){
		movablePart.constraints = RigidbodyConstraints2D.FreezeRotation| RigidbodyConstraints2D.FreezePositionX;

		float distanceToTarget = Vector3.Distance (movablePart.transform.position, target);

		while (distanceToTarget>0.05f) {
			//recalculate the distance between the element's position to the target's position
			distanceToTarget = Vector3.Distance (movablePart.transform.position, target);
			
			//Create a vector that point to target
			Vector3 vel = (target - movablePart.transform.position);
			//Normalize the vector
			vel.Normalize ();

			movablePart.AddForce(new Vector2(vel.x,vel.y) * maxForce);

			if(movablePart.velocity.magnitude > maxSpeed){
				movablePart.velocity = Vector2.ClampMagnitude( movablePart.velocity, maxSpeed);
			}

			yield return null;
		}

		movablePart.constraints = RigidbodyConstraints2D.FreezeAll;


>>>>>>> origin/master
	}
}
