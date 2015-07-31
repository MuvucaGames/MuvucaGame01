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
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Game.LoadLevel(GameLevel.MainMenu);
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            ToggleAirControl();
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

    void ToggleAirControl()
    {
        PlatformerCharacter2D main = GetMain().GetComponent<PlatformerCharacter2D>();
        main.m_AirControl = !main.m_AirControl; //negate the current value

        PlatformerCharacter2D back = GetMain().GetComponent<PlatformerCharacter2D>();
        back.m_AirControl = main.m_AirControl; //defines as equals the other
    }

    private GameObject GetMain()
    {
        return controllingGroom ? groom : bride;
    }

    private GameObject GetBack()
    {
        return controllingGroom ? bride: groom;
    }

}
