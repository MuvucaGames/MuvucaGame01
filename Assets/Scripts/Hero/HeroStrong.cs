using UnityEngine;
using System.Collections;

public class HeroStrong : Hero {

	protected override void Awake(){
		base.Awake ();
		IsActive = true;
	}

	public override bool IsStrong ()
	{
		return true;
	}

}
