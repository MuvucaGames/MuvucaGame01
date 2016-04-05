using UnityEngine;
using System.Collections;
using System;

public class HeroControlTemp : MonoBehaviour {
	
	private static float MOVE_THRESHOLD = 0.19f;
	
	private HeroTemp hero;
	
	// Input States
	private float _moveButtonSpeed = 0.0f;
	private bool _jumpButtonPressed = false;
	private bool _crouchButtonPressed = false;
	private bool _carryButtonPressed = false;
	private bool _actionButtonPressed = false;
	private bool _changeHeroButtonPressed = false;
	private ElementoCarregavel _elementoCarregavel;
	private bool _cannCarry = false;
	
	
	void Awake () {
		hero = GetComponent<HeroTemp> ();
	}
	
	private void detectButtonStates(){
		//Analog Input:
		// Walk
		_moveButtonSpeed = Input.GetAxis("Horizontal");
		
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
		
		// Change Hero
		if (Input.GetButtonDown("ChangeHero"))
			_changeHeroButtonPressed = true;
		
		// Jump
		if (Input.GetButtonDown("Jump"))
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
			if (_changeHeroButtonPressed) {
				hero.ChangeHero();
			}
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
			if(_elementoCarregavel != null && _elementoCarregavel.HeroInn){
				_cannCarry = true;
			}
		} else {
			if(_cannCarry){
				_elementoCarregavel.SendMessage("adicionaGravidade");
			}
			_cannCarry = false;
			hero.StopCarry();
		}



		if(_cannCarry){
			hero.LevantaElementoCarregavel(_elementoCarregavel);
		}
		
		if (_changeHeroButtonPressed) {
			hero.ChangeHero();
			SoundManager.Instance.SendMessage("PlaySFXSwap");
		}
		
		if (_jumpButtonPressed) {
			hero.Jump ();
		}
		
		if (_actionButtonPressed) {
			hero.Action();
		}
		
		if (Mathf.Abs (_moveButtonSpeed) > HeroControlTemp.MOVE_THRESHOLD) {
			hero.Move (_moveButtonSpeed);
		} else {
			hero.Move (0.0f);
		}
		
		DeactivateTriggerButtons();
	}
	
	private void DeactivateTriggerButtons(){
		
		_changeHeroButtonPressed = false;
		_jumpButtonPressed = false;
		_actionButtonPressed = false;
	}

	public void TriggerEscada(ElementoCarregavel elemento){
		//if (_carryButtonPressed) {
			_elementoCarregavel = elemento;
		//}
	}

}
