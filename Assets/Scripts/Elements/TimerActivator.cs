using UnityEngine;
using System.Collections;

public class TimerActivator : ElementActivator
{

	public void Activation(){
		ActivateAll ();
	}

	public void Deactivation(){
		DeactivateAll ();
	}
}

