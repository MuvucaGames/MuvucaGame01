using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectSlot : MonoBehaviour {

	//Placeholder to gameManager
	bool[] savedGames;
	bool isNewGame;

	// Use this for initialization
	void Start () {
		isNewGame = true;

		savedGames = new bool[4];
		savedGames [2] = true;

		for (int i = 0; i < savedGames.Length; i++) {
			if (savedGames[i]){
				GameObject.Find("Slot"+(i+1)).transform.Find("Image").GetComponent<Image>().color = Color.blue;
			}
		}
	}

	public void SlotButton(Button btn){
		int id = (int)char.GetNumericValue(btn.name[btn.name.Length-1]);
		Debug.Log (id);

		if (isNewGame) {
			if (savedGames [id-1]) {
				Debug.Log("saved");
				//toDo show overwite popup
			} else {
				Debug.Log("new");
				//Application.LoadLevel ("Test");
			}
		} else {
			if (savedGames [id-1]) {
				Debug.Log("load");
				//Application.LoadLevel ("Test");
			}
		}
	}

	public void BackButton(){
		Application.LoadLevel ("MainMenu");
	}
}
