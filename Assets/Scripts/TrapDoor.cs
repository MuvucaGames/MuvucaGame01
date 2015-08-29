using UnityEngine;
using System.Collections;

public class TrapDoor : ActionableElement
{

	private bool done = false;

	private Animator animator;

	void Awake ()
	{
		animator = this.GetComponentInChildren<Animator> ();
		animator.SetBool ("opened", false);
	}

	public override void Activate ()
	{
		if (!done) {
			this.GetComponentInChildren<BoxCollider2D> ().enabled = false;
			animator.SetBool ("opened", true);
			done = true;
		}
	}

	public override void Deactivate ()
	{

	}
}
