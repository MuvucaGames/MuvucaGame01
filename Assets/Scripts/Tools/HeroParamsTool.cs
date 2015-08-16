using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HeroParamsTool : MonoBehaviour {

	[SerializeField] private Hero hero = null;

	public Slider jumpHeightSlider;
	public Text jumpHeightText;

	public Slider maxWalkingSpeedSlider;
	public Text maxWalkingSpeedText;

	public Slider maxMotorTorqueSlider;
	public Text maxMotorTorqueText;

	public Slider horizontalFlyingForceSlider;
	public Text horizontalFlyingForceText;

	public Slider gravityScaleSlider;
	public Text gravityScaleText;

	// Use this for initialization
	void Start () {
		InitUI ();
	}
	
	private void InitUI(){
		jumpHeightSlider.value = hero.JumpHeight;

		maxWalkingSpeedSlider.value = hero.MaxWalkingSpeed;

		maxMotorTorqueSlider.value = hero.WalkMotorTorque;

		horizontalFlyingForceSlider.value = hero.HorizontalFlyingForce;

		gravityScaleSlider.value = hero.GravityScale;
	}

	public void ChangeJumpHeight(float f){
		hero.JumpHeight = f;
		jumpHeightText.text = f.ToString("F2");
	}

	public void ChangeMaxWalkingSpeed(float f){
		hero.MaxWalkingSpeed = f;
		maxWalkingSpeedText.text = f.ToString("F2");
	}

	public void ChangeTorque(float f){
		hero.WalkMotorTorque = f;
		maxMotorTorqueText.text = f.ToString("F2");
	}

	public void ChangeHorizontalFlyingForce(float f){
		hero.HorizontalFlyingForce = f;
		horizontalFlyingForceText.text = f.ToString("F2");
	}

	public void ChangeGravityScale(float f){
		hero.GravityScale = f;
		print(f.ToString("F2"));
		gravityScaleText.text = f.ToString("F2");
	}




}
