using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour {

	[SerializeField]private Transform heroStrongTransform = null;
	[SerializeField]private Transform heroFastTransform = null;

	[SerializeField]private GameObject notCheckedRenderer = null;
	[SerializeField]private GameObject oneCheckedRenderer = null;
	[SerializeField]private GameObject bothCheckedRenderer = null;


	public int checkpointOrder;
	public bool requiresBothHeroes = true;
	private List<GameObject> checkedHeroes = new List<GameObject> ();

	public GameObject manager;

	public void Awake(){
		//Remove renderers from chackpoint helpers;
		heroFastTransform.GetComponent<SpriteRenderer>().enabled = false;
		heroStrongTransform.GetComponent<SpriteRenderer>().enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		GameObject collidedHero = other.transform.parent.gameObject;
		if (!checkedHeroes.Contains(collidedHero))
	    {
			checkedHeroes.Add (collidedHero);
			ChangeRendering();
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

	private void ChangeRendering(){
		if (requiresBothHeroes && checkedHeroes.Count < 2) {
			notCheckedRenderer.SetActive(false);
			oneCheckedRenderer.SetActive(true);
			bothCheckedRenderer.SetActive(false);
		} else {
			notCheckedRenderer.SetActive(false);
			oneCheckedRenderer.SetActive(false);
			bothCheckedRenderer.SetActive(true);
		}

	}

	public Transform HeroStrongTransform {
		get {
			return this.heroStrongTransform;
		}
	}

	public Transform HeroFastTransform {
		get {
			return this.heroFastTransform;
		}
	}


}
