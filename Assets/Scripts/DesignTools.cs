using UnityEngine;
using System.Collections;

public class DesignTools : MonoBehaviour {

	public Hero fastHero;
	public Hero strongHero;
	public CheckpointManager cm;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TooglePanel(){
		GameObject panel = transform.FindChild ("Panel").gameObject;
		panel.SetActive (!panel.activeSelf);
	}

	public void ResetLevel(){
		//TODO fix this mess
		cm.ResetCheckpoins ();
		Application.LoadLevel(Application.loadedLevel);
	}
}
