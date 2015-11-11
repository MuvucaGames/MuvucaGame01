using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour {

	private Transform heroStrongTransform;
	private Transform heroFastTransform;

	private GameObject notCheckedRenderer;
	private GameObject oneCheckedRenderer;
	private GameObject bothCheckedRenderer;


	public int checkpointOrder;
	public bool requiresBothHeroes = true;
	private List<GameObject> checkedHeroes = new List<GameObject> ();

	public GameObject manager;

	public void Awake(){
		heroFastTransform = transform.FindChild ("FastHeroRestartPosition");
		heroStrongTransform = transform.FindChild ("StrongHeroRestartPosition");

		Transform particleTransform = this.transform.FindChild ("ParticulesRenderers");
		notCheckedRenderer = particleTransform.FindChild ("NotChecked").gameObject;
		oneCheckedRenderer = particleTransform.FindChild("OneChecked").gameObject;
		bothCheckedRenderer = particleTransform.FindChild ("Checked").gameObject;

		//Remove renderers from chackpoint helpers;
		heroFastTransform.GetComponent<SpriteRenderer>().enabled = false;
		heroStrongTransform.GetComponent<SpriteRenderer>().enabled = false;
	}

	public void Start(){

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
