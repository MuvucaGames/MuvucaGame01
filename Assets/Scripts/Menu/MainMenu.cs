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
        //Game.LoadLevel(GameLevel.SelectSlot);
	}

	public void LoadGameButton(){
        //Game.LoadLevel(GameLevel.SelectSlot);
	}

	public void OptionsButton(){
        //Game.LoadLevel(GameLevel.Options);
	}

	public void CreditsButton(){
        //Game.LoadLevel(GameLevel.Credits);
	}

	public void ExitGameButton(){
        //Game.Quit();
	}
}
