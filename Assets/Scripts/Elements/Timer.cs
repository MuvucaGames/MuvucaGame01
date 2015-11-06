using UnityEngine;
using System.Collections;

public class Timer : ActionableElement {

	private float timeLeftInSeconds;
	private bool isActive;
	private TimerActivator timerActivator;
	public int initialTimeInSeconds;

	public override void Activate()
	{
		timerActivator = GetComponent<TimerActivator> ();
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
		timerActivator.Activation ();
	}

	void resetTimer() {
		timerActivator.Deactivation ();
	}
}
