using UnityEngine;
using System.Collections;
using System;

public class HeroControl : MonoBehaviour {

	private static float MOVE_THRESHOLD = 0.19f;

	private Hero hero;

	// Input States
	private float _moveButtonSpeed = 0.0f;
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
		_moveButtonSpeed = Input.GetAxis("Horizontal");

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

	void FixedUpdate () {

		if (!hero.IsActive) {
			hero.StopWalk();
			return;
		}

		if (_changeHeroButtonPressed) {
			hero.ChangeHero();
			SoundManager.Instance.SendMessage("PlaySFXSwap");
		}

		if (_jumpButtonPressed) {
			hero.Jump ();
		}

		if (_crouchButtonPressed) {
			hero.Crouch ();
		} else {
			hero.StandUp();
		}

		if (_pushButtonPressed) {
			hero.Push();
		}

		if (_actionButtonPressed) {
			hero.Action();
		}

		if (Mathf.Abs (_moveButtonSpeed) > HeroControl.MOVE_THRESHOLD) {
			hero.Move (_moveButtonSpeed);
		} else {
			hero.Move (0.0f);
		}


	}
}
