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
	}
	
	void FixedUpdate () {

		hero.Move (Input.GetAxis("Horizontal"), Input.GetKey(KeyCode.DownArrow), m_Jump);

		m_Jump = false;
	}
}
