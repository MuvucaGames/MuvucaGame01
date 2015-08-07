using UnityEngine;
using System.Collections;

public class HeroControl : MonoBehaviour {

	private Hero hero;

	private bool m_Jump;

	void Awake () {
		hero = GetComponent<Hero> ();
	}

	void Update(){
		if (!m_Jump)
		{
			// Read the jump input in Update so button presses aren't missed.
			m_Jump = Input.GetKeyDown(KeyCode.Space);
		}

		if (Input.GetKeyDown (KeyCode.Tab))
			hero.ChangeHero ();
	}
	
	void FixedUpdate () {

		if(hero.IsActive)
			hero.Move (Input.GetAxis("Horizontal"), Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.S), m_Jump);

		m_Jump = false;
	}
}
