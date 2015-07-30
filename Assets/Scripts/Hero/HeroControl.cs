using UnityEngine;
using System.Collections;

public class HeroControl : MonoBehaviour {

	private Hero hero;

	private bool m_Jump;

	// Use this for initialization
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
	
	// Update is called once per frame
	void FixedUpdate () {

		hero.Move (Input.GetAxis("Horizontal"), false, m_Jump);

		m_Jump = false;
	}
}
