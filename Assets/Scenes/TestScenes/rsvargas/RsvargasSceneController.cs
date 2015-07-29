using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class RsvargasSceneController : MonoBehaviour {

	[Range(0,1)]
	public float backgroundPlayerAlpha = 0.7f;

	GameObject groom;
	GameObject bride;
	JumpMeasurer measurer;
	bool controllingGroom = true;


	// Use this for initialization
	void Start () {
		groom = GameObject.Find ("TestGroom");
		bride = GameObject.Find ("TestBride");
		measurer = GameObject.Find ("JumpMeasurer").GetComponent<JumpMeasurer> ();
		SetPlayer (controllingGroom);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			controllingGroom = !controllingGroom;
			SetPlayer (controllingGroom);
		}
	}

	void SetPlayer(bool toGroom)
	{
		GameObject main = toGroom ? groom : bride;
		GameObject back = toGroom ? bride : groom;

		main.GetComponent<Platformer2DUserControl> ().enabled = true;
		back.GetComponent<Platformer2DUserControl> ().enabled = false;

		SpriteRenderer mainRend = main.GetComponentInChildren<SpriteRenderer> ();
		mainRend.color = Color.white;
		mainRend.sortingOrder = 1;
		SpriteRenderer backRend = back.GetComponentInChildren<SpriteRenderer> ();
		backRend.color = new Color(1f,1f,1f,backgroundPlayerAlpha);
		backRend.sortingOrder = 0;

		measurer.tracker = main.transform;

	}
}
