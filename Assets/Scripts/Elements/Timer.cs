using UnityEngine;
using System.Collections;

public class Timer : ActionableElement {

	private float timeLeftInSeconds;
	private bool isActive;

	public int initialTimeInSeconds;
	public ActionableElement actionableElement;

	public override void Activate()
	{
		isActive = true;
	}

	public override void Deactivate()
	{
		isActive = false;
	}

	void Start () {
		timeLeftInSeconds = initialTimeInSeconds;
	}
	
	void Update () {
		if (isActive) {
			timeLeftInSeconds -= Time.deltaTime;
		}

	
		if (timeLeftInSeconds <= 0) {
			timeout ();
		} 
	}

	void timeout() {
		actionableElement.Activate ();
	}

	void resetTimer() {
		actionableElement.Deactivate ();
	}
}
