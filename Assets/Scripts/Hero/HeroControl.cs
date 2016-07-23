using UnityEngine;
using System.Collections;
using System;

public class HeroControl : MonoBehaviour {

	private static float MOVE_THRESHOLD = 0.19f;
	private Hero hero;

	void Awake () {
		hero = GetComponent<Hero> ();
	}

    // Its called once per frame
    public void ProcessHeroInput(game_input GameInput)
    {
		if (!hero.IsActive) {
			return;
		}

		if (GameInput.crouch) {
			hero.Crouch ();
		} else {
			hero.StandUp();
		}
		
		if (GameInput.carry) {
			hero.Carry ();
		} else {
			hero.StopCarry();
		}

        if (GameInput.jump)
        {
            hero.Jump ();
        }            

		if (GameInput.action) {
			hero.Action();
		}

		if (Mathf.Abs (GameInput.horizontalAxis) > HeroControl.MOVE_THRESHOLD) {
			hero.Move (GameInput.horizontalAxis);
		} else {
			hero.Move (0.0f);
		}
		if (Mathf.Abs (GameInput.verticalAxis) > HeroControl.MOVE_THRESHOLD) {
			hero.VerticalMove (GameInput.verticalAxis);
		} else {
			hero.VerticalMove (0.0f);
		}

        if (GameInput.verticalAxis != 0)
        {
            hero.CarryBox();
        }

	}

}