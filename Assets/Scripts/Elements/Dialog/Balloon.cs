using UnityEngine;
using System.Collections;

public class Balloon : ActionableElement {

	public Transform speaker;
	public Vector2 speakerOffset;
	public int timeToLiveInSeconds;
	private float timeLeft = 0;

	void Update () 
	{
		if (timeLeft > 0) {
			Vector3 position = new Vector3 (speaker.position.x + speakerOffset.x, speaker.position.y + speakerOffset.y, 0);
			gameObject.transform.position = position;
		} 
		else
		{
			Vector3 position = new Vector3 (1000,1000,1000);
			gameObject.transform.position = position;
		}
		timeLeft = timeLeft - Time.deltaTime;
	}

	public override void Activate()
	{
		timeLeft = timeToLiveInSeconds;
	}

	public override void Deactivate()
	{
		//Do nothing
	}
}
