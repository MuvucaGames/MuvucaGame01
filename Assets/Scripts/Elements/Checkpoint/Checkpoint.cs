using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour {
	public Vector3 heroStrongPosition;
	public Vector3 heroFastPosition;
	public int checkpointOrder;
	public bool requiresBothHeroes = true;
	private List<GameObject> checkedHeroes = new List<GameObject> ();

	public GameObject manager;

	void OnTriggerEnter2D(Collider2D other) {
		GameObject collidedHero = other.transform.parent.gameObject;
		if (!checkedHeroes.Contains(collidedHero))
	    {
			checkedHeroes.Add (collidedHero);
		}

		if (requiresBothHeroes) 
		{
			if (checkedHeroes.Count == 2)
			{
				CheckThisPoint();
			}
		} 
		else
		{
			CheckThisPoint();
		}
	}

	private void CheckThisPoint()
	{
		CheckpointManager script = manager.GetComponent<CheckpointManager> ();
		script.RegisterCheckpoint (this);
	}
}
