using UnityEngine;
using System.Collections;

public class HeroStrongTemp : HeroTemp {
	
	protected override void Awake(){
		base.Awake ();
		CalculateHeroHeight();
		IsActive = true;
	}
	
}
