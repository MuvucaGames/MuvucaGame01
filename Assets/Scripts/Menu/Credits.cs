using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Credits : MonoBehaviour {

	private RectTransform listTransform, creditsTransform;
	private Text text;
	private string list;
	private float textHeight, screenHeight;
	public float rollSpeed;
	
	void Start () {
		list = Resources.Load ("Credits List").ToString();
		list = list.Replace('"', ' ').Trim ();

		text = GameObject.Find ("List").GetComponent<Text> ();
		text.text = list;

		textHeight = GameObject.Find ("List").GetComponent<Text> ().preferredHeight;
		screenHeight = Screen.height;

		listTransform = GameObject.Find ("List").GetComponent<RectTransform> ();
		creditsTransform = GameObject.Find ("Credits").GetComponent<RectTransform> ();

		creditsTransform.position = new Vector3(creditsTransform.position.x, -100, creditsTransform.position.z);
		listTransform.position = new Vector3(listTransform.position.x, -165, listTransform.position.z);
	}

	void FixedUpdate () {
		if (listTransform.position.y > textHeight + screenHeight){
            //TODO
			//Game.LoadLevel(GameLevel.MainMenu);
		}

		listTransform.position += new Vector3 (0, rollSpeed);
		creditsTransform.position += new Vector3 (0, rollSpeed);
	}

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Escape))
        {
			//TODO
            //Game.LoadLevel(GameLevel.MainMenu);
        }
    }
}
