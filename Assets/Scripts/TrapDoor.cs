using UnityEngine;
using System.Collections;

public class TrapDoor : ActionableElement {


	public Transform rotateAxis;

	private bool done = false;

	public override void Activate ()
	{
		if (!done) {
			this.transform.RotateAround ( rotateAxis.position, new Vector3(0,0,1), -90);
			done = true;
		}

	}

	public override void Deactivate ()
	{

	}
}
