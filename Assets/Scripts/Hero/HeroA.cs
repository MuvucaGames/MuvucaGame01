using UnityEngine;
using System.Collections;

public class HeroA : Hero {

	protected override void Awake(){
		base.Awake ();
		IsActive = true;
	}

}
