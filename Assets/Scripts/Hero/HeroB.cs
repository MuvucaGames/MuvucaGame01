using UnityEngine;
using System.Collections;

public class HeroB : Hero {

	protected override void Awake(){
		base.Awake ();
		IsActive = true;
	}
}
