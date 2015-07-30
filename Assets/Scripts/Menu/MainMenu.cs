using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	//Checkbox on inspector to test if there is a saved game
	public bool existSavedGame;

	// Use this for initialization
	void Start () {
		if (!existSavedGame) {
			GameObject.Find("ContinueButton").SetActive(false);
		}
	}

	public void ContinueButton(){
		//Application.LoadLevel("Test");
		Debug.Log("Continue");
	}

	public void NewGameButton(){
		Application.LoadLevel("SelectSlot");
		Debug.Log("New Game");
	}

	public void LoadGameButton(){
		Application.LoadLevel("SelectSlot");
		Debug.Log("Load Game");
	}

	public void OptionsButton(){
		Application.LoadLevel("Options");
		Debug.Log("Options");
	}

	public void CreditsButton(){
		Debug.Log("Credits");
		Application.LoadLevel("Credits");
	}

	public void ExitGameButton(){
		Debug.Log("Exit Game");
		Application.Quit ();
	}
}
