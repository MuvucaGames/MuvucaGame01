using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DesignTools : MonoBehaviour {

	public Hero fastHero;
	public Hero strongHero;
	public CheckpointManager checkpointManager;

	public Text sh_jumpHeight;
	public Text fh_jumpHeight;

	public Text sh_walkForce;
	public Text fh_walkForce;

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
		checkpointManager.ResetCheckpoins ();
		Application.LoadLevel(Application.loadedLevel);
	}

	public void SetFastHeroJumpHeight(float f){
		fastHero.JumpHeight = f;
		fh_jumpHeight.text = f.ToString ();
	}

	public void SetStrongHeroJumpHeight(float f){
		strongHero.JumpHeight = f;
		sh_jumpHeight.text = f.ToString ();
	}

	public void SetFastHeroWalkForce(float f){
		fastHero.WalkMotorTorque = f;
		fh_walkForce.text = f.ToString ();
	}

	public void SetStrongHeroWalkForce(float f){
		strongHero.WalkMotorTorque = f;
		sh_walkForce.text = f.ToString ();
	}

}
