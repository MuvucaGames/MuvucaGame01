using UnityEngine;
using System.Collections;

/// <summary>
///   Input of shared information treatment.
/// </summary>
/// Sensitive information which is shared between multiple instances must be treated
/// with exclusive access.
///
/// Button for changing focus of keyboard and camera depends on who will be the next active
/// controllable object. If next active is a Hero, we use button 'ChangeHero'. If next active is
/// some other controllable object, for example, Claw, then user will change input and camera 
/// using the 'Action' button.
///
/// \sa Claw, ColorsPuzzle, Hero

public struct game_input
{
    public bool jump;
    public bool crouch;
    public bool action;
    public bool carry;
    public bool changeHero;

    // Axis
    public float horizontalAxis;
    public float verticalAxis;

    // TODO: Implement Pause button and pause screen
    public bool pause;
}

public class InputManager : MonoBehaviour
{    
    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            Debug.AssertFormat(instance != null, "Instance of InputManager is not initialized");
            return instance;
        }
    }

    public HeroStrong heroStrong = null;
    public HeroFast heroFast = null;
    public HeroControl heroStrongControl = null;
    public HeroControl heroFastControl = null;

	// Input States
    public game_input GameInput;

	void Start () 
    {
        instance = this;
        heroStrong = FindObjectOfType<HeroStrong>();
        heroFast = FindObjectOfType<HeroFast>();
        heroStrongControl = heroStrong.GetComponent<HeroControl>();
        heroFastControl = heroFast.GetComponent<HeroControl>();
    }

	private void detectButtonStates(){

		//Analog Input:
		GameInput.horizontalAxis = Input.GetAxis("Horizontal");
		
		GameInput.verticalAxis = Input.GetAxis("Vertical");
		
		//-------------------------------------------

		//Boolean Buttons, or teo state buttons:
		// Crouch

		if (Input.GetButton("Crouch"))
			GameInput.crouch = true;
		else
			GameInput.crouch = false;
		
		// Push
		if (Input.GetButton("Carry"))
			GameInput.carry = true;
		else
			GameInput.carry = false;

		//----------------------------------------

		//Trigger Buttons, one time activation: (they are deactivated on Fixed Update)

		// Jump
		if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
			GameInput.jump = true;

		// Action
		if (Input.GetButtonDown("Action"))
			GameInput.action = true;

		// Change Hero
		if (Input.GetButtonDown("ChangeHero"))
			GameInput.changeHero = true;

	}

    void Update()
    {
		detectButtonStates();
    }

    void FixedUpdate()
    {
        heroStrongControl.ProcessHeroInput(GameInput);
        heroFastControl.ProcessHeroInput(GameInput);

        DeactivateTriggerButtons();

		if (GameInput.changeHero) 
        {
			HeroUtil.ChangeHero();
            GameInput.changeHero = false;
		}
    }

	private void DeactivateTriggerButtons(){
		
		GameInput.jump = false;
		GameInput.action = false;
	}
}
