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
	private bool _changeHeroButtonPressed = false;
	private bool _actionButtonPressed = false;

	void Start () 
    {
        instance = this;
        heroStrong = FindObjectOfType<HeroStrong>();
        heroFast = FindObjectOfType<HeroFast>();
    }

    void Update()
    {
		// Change Hero
		if (Input.GetButtonDown("ChangeHero"))
			_changeHeroButtonPressed = true;
    }

    void FixedUpdate()
    {
		if (_changeHeroButtonPressed) 
        {
			HeroUtil.ChangeHero();
            _changeHeroButtonPressed = false;
			SoundManager.Instance.SendMessage("PlaySFXSwap");
		}
    }
}