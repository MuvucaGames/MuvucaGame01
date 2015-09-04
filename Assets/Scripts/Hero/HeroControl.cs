using UnityEngine;
using System.Collections;

public class HeroControl : MonoBehaviour {

	private Hero hero;

	// Input States
	private float _walkButtonPressed = 0.0f;
	private bool _jumpButtonPressed = false;
	private bool _crouchButtonPressed = false;
	private bool _pushButtonPressed = false;
	private bool _actionButtonPressed = false;
	private bool _changeHeroButtonPressed = false;


	void Awake () {
		hero = GetComponent<Hero> ();
	}

	private void detectButtonStates(){
		// Walk
		if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.0f)
			_walkButtonPressed = Input.GetAxis("Horizontal");

		// Change Hero
		if (Input.GetButtonDown("ChangeHero"))
			_changeHeroButtonPressed = true;
		if (Input.GetButtonUp("ChangeHero"))
			_changeHeroButtonPressed = false;

		// Jump
		if (Input.GetButtonDown("Jump"))
			_jumpButtonPressed= true;
		if (Input.GetButtonUp("Jump"))
			_jumpButtonPressed = false;

		// Crouch
		if (Input.GetButtonDown("Crouch"))
			_crouchButtonPressed= true;
		if (Input.GetButtonUp("Crouch"))
			_crouchButtonPressed = false;

		// Push
		if (Input.GetButtonDown("Push"))
			_pushButtonPressed= true;
		if (Input.GetButtonUp("Push"))
			_pushButtonPressed = false;

		// Action
		if (Input.GetButtonDown("Action"))
			_actionButtonPressed= true;
		if (Input.GetButtonUp("Action"))
			_actionButtonPressed = false;
	}

	void Update() {
		detectButtonStates();
	}

	/*void Update(){
		if (!m_Jump)
		{
			// Read the jump input in Update so button presses aren't missed.
			m_Jump = Input.GetButtonDown("Jump");
			Debug.Log("Jump=" + m_Jump);
		}

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            hero.ChangeHero();
            SoundManager.Instance.SendMessage("PlaySFXSwap");
        }

        if (Input.GetButtonDown("Action") && hero.IsActive)
        {
            hero.SendMessage("DoAction");
        }
	}*/
	
	void FixedUpdate () {

		if (_changeHeroButtonPressed) {
			hero.ChangeHero();
			SoundManager.Instance.SendMessage("PlaySFXSwap");
		}

		if (_actionButtonPressed) {
			hero.SendMessage("DoAction");
		}


		if (hero.IsActive)
			hero.Move(_walkButtonPressed, _crouchButtonPressed, _jumpButtonPressed);
		else
			hero.StopWalk ();

		_jumpButtonPressed = false;
	}
}
