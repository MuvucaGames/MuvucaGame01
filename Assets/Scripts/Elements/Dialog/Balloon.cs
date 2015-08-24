using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {

	public Transform speaker;
	public Vector2 speakerOffset;
	public int timeToLiveInSeconds;
	private float timeLeft;

	void Update () {
		Vector3 position = new Vector3(speaker.position.x + speakerOffset.x, speaker.position.y + speakerOffset.y, 0);
		gameObject.transform.position = position;
	}
}
