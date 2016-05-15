using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

	public GameObject graphicsPanel, controlsPanel, soundPanel;

	// Use this for initialization
	void Start () {
	
	}

	public void GraphicsButton (){
		soundPanel.SetActive (false);
		controlsPanel.SetActive (false);
		graphicsPanel.SetActive (true);
		Debug.Log ("Graphics");
	}

	public void SoundButton (){
		soundPanel.SetActive (true);
		controlsPanel.SetActive (false);
		graphicsPanel.SetActive (false);
		Debug.Log ("Sound");
	}

	public void ControlsButton (){
		soundPanel.SetActive (false);
		controlsPanel.SetActive (true);
		graphicsPanel.SetActive (false);
		Debug.Log ("Controls");
	}

	public void RestoreButton (){
		Debug.Log ("Restore to Default");
	}
	
	public void BackButton(){
        //Game.LoadLevel(GameLevel.MainMenu);
	}
}
