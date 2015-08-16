using UnityEngine;
using System.Collections;

public class Door : ActionableElement {
	private Vector3 closedPosition;
	private Vector3 openedPosition;
	void Awake()
	{
		closedPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		openedPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 4, gameObject.transform.position.z);
	}

	public override void Activate()
	{
		gameObject.transform.position = openedPosition;
		gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
	}

	public override void Deactivate()
	{
		gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
	}

}
