using UnityEngine;
using System.Collections;

public class WeigthButton : MonoBehaviour {
	public GameObject actionableObject;
	public double activationWeight;
	private double weightAboveMe = 0;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (IsCollisionAboveMe (collision)) 
		{
			AddWeightFromCollider(collision.collider);
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (IsCollisionAboveMe (collision)) 
		{
			RemoveWeightFromCollider(collision.collider);
		}
	}

	private bool IsCollisionAboveMe(Collision2D collision)
	{
		ContactPoint2D[] contacts = collision.contacts;
		foreach (ContactPoint2D c in contacts) 
		{
			if (c.normal == Vector2.down) {
				return true;
			}
		}
		return false;
	}

	private void AddWeightFromCollider(Collider2D collider)
	{
		double colliderMass = collider.transform.parent.GetComponent<Rigidbody2D>().mass;
		weightAboveMe = weightAboveMe + colliderMass;
		CheckActivationStatus ();
	}

	private void RemoveWeightFromCollider(Collider2D collider)
	{
		double colliderMass = collider.transform.parent.GetComponent<Rigidbody2D>().mass;
		weightAboveMe = weightAboveMe - colliderMass;
		if (weightAboveMe < 0) 
		{
			weightAboveMe = 0;
		}
		CheckActivationStatus ();
	}

	private void CheckActivationStatus()
	{
		print (weightAboveMe);
		IActionableElement actionableElement = actionableObject.GetComponent<IActionableElement> ();
		if (weightAboveMe >= activationWeight) {
			actionableElement.Activate ();
		} else 
		{
			actionableElement.Deactivate ();
		}
	}

}
