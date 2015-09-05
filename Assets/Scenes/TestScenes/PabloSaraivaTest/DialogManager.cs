using UnityEngine;
using System.Collections;

public class DialogManager : ActionableElement 
{
	public Balloon balloonPrefab;
	public Transform hookPosition;

	public override void Activate()
	{
		Balloon instance = (Balloon)Instantiate (balloonPrefab, new Vector3(0,0,0), Quaternion.identity);
		instance.speaker = hookPosition;
		instance.Activate ();
	}

	public override void Deactivate()
	{

	}

}
