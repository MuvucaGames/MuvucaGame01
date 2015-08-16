using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WeigthButton : MonoBehaviour {
    public ActionableElement[] actionableObjects;
	public double activationWeight;
	private double weightAboveMe = 0;
	private IList<Rigidbody2D> bodyList = new List<Rigidbody2D> ();

	void FixedUpdate()
	{
		CheckActivationStatus ();
		bodyList.Clear ();
		weightAboveMe = 0;
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if (IsCollisionAboveMe (collision)) 
		{
			AddBodies(collision.collider);
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

	private void SumWeightFromBodies()
	{
		foreach (Rigidbody2D rb in bodyList) 
		{
			weightAboveMe = weightAboveMe + rb.mass;
		}
	}

	private void CheckActivationStatus()
	{
		SumWeightFromBodies ();
        foreach (ActionableElement actionableElement in actionableObjects)
        {
            if (actionableElement == null) return;
            if (weightAboveMe >= activationWeight)
            {
                actionableElement.Activate();
            }
            else
            {
                actionableElement.Deactivate();
            }
        }
		
	}

	private Rigidbody2D ExtractRigidbody(Collider2D collider)
	{
		return collider.GetComponentInParent<Rigidbody2D> ();
	}

	private void AddBodies(Collider2D collider)
	{
		Rigidbody2D rigidbody = ExtractRigidbody (collider);
		if (rigidbody == null) 
		{
			return;
		}

		if (!bodyList.Contains (rigidbody)) {
			bodyList.Add (rigidbody);
			RaycastHit2D[] raycastHits = Physics2D.BoxCastAll (collider.transform.position, new Vector2(1, 0.4f), 0, Vector2.up, 0.4f);
			foreach (RaycastHit2D hit in raycastHits)
			{
				AddBodies(hit.collider);
			}
		}
	}
}
