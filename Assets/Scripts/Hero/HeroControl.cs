using UnityEngine;
using System.Collections;
using System;

public class HeroControl : MonoBehaviour {

	private static float MOVE_THRESHOLD = 0.19f;

	private Hero hero;

	// Input States
	private float _moveButtonSpeed = 0.0f;
	private float _verticalMoveButtonSpeed = 0.0f;
	private bool _jumpButtonPressed = false;
	private bool _crouchButtonPressed = false;
	private bool _carryButtonPressed = false;
	private bool _actionButtonPressed = false;


	void Awake () {
		hero = GetComponent<Hero> ();
	}

	private void detectButtonStates(){
		//Analog Input:
		// Walk
		_moveButtonSpeed = Input.GetAxis("Horizontal");
		
		_verticalMoveButtonSpeed = Input.GetAxis("Vertical");
		
		//-------------------------------------------

		//Boolean Buttons, or teo state buttons:
		// Crouch
		if (Input.GetButton("Crouch"))
			_crouchButtonPressed= true;
		else
			_crouchButtonPressed = false;
		
		// Push
		if (Input.GetButton("Carry"))
			_carryButtonPressed= true;
		else
			_carryButtonPressed = false;

		//----------------------------------------

		//Trigger Buttons, one time activation: (they are deactivated on Fixed Update)

		// Jump
		if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
			_jumpButtonPressed= true;

		// Action
		if (Input.GetButtonDown("Action"))
			_actionButtonPressed= true;
	}

	void Update() {
		detectButtonStates();
	}

	void FixedUpdate () {

		if (!hero.IsActive) {
			hero.StopWalk();
			DeactivateTriggerButtons();
			return;
		}

		if (_crouchButtonPressed) {
			hero.Crouch ();
		} else {
			hero.StandUp();
		}
		
		if (_carryButtonPressed) {
			hero.Carry ();
		} else {
			hero.StopCarry();
		}

		if (_jumpButtonPressed) {
			hero.Jump ();
		}

		if (_actionButtonPressed) {
			hero.Action();
		}

		if (Mathf.Abs (_moveButtonSpeed) > HeroControl.MOVE_THRESHOLD) {
			hero.Move (_moveButtonSpeed);
		} else {
			hero.Move (0.0f);
		}
		if (Mathf.Abs (_verticalMoveButtonSpeed) > HeroControl.MOVE_THRESHOLD) {
			hero.VerticalMove (_verticalMoveButtonSpeed);
		} else {
			hero.VerticalMove (0.0f);
		}

		DeactivateTriggerButtons();
	}

	private void DeactivateTriggerButtons(){
		
		_jumpButtonPressed = false;
		_actionButtonPressed = false;
	}
}