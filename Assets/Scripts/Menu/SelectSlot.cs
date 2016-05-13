using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectSlot : MonoBehaviour {

	//Placeholder to gameManager
    public Sprite newFile;
    public Sprite loadFile;

	bool isNewGame;

	// Use this for initialization
	void Start () {
		isNewGame = true;
        
        //GameData[] used = Game.savedGames;
		/*
		for (int i = 0; i < used.Length; i++) {
            GameObject slot = GameObject.Find("Slot"+(used[i].Slot));
            Image img = slot.transform.Find("Image").GetComponent<Image>();
            //img.color = Color.blue;
            if (used[i].Free)
            {
                img.sprite = newFile;
            }
            else
            {
                img.sprite = loadFile;
            }

		}*/
	}

	public void SlotButton(Button btn){
		int id = (int)char.GetNumericValue(btn.name[btn.name.Length-1]);
		Debug.Log ("Starting slot " + id);

        //Game.LoadSlot(id - 1);

        //if (isNewGame) {
        //    if (savedGames [id-1]) {
        //        Debug.Log("saved");
        //        //toDo show overwite popup
        //    } else {
        //        Debug.Log("new");
        //        //Application.LoadLevel ("Test");
        //    }
        //} else {
        //    if (savedGames [id-1]) {
        //        Debug.Log("load");
        //        //Application.LoadLevel ("Test");
        //    }
        //}
	}

	public void BackButton(){
        //Game.LoadLevel(GameLevel.MainMenu);
	}
}
