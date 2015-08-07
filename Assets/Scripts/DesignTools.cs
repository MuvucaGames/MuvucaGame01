using UnityEngine;
using System.Collections;

public class DesignTools : MonoBehaviour {

	public Hero fastHero;
	public Hero strongHero;

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
}
