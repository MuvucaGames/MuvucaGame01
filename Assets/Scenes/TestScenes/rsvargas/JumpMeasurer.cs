using UnityEngine;
using System.Collections;


public class JumpMeasurer : MonoBehaviour {

	public float linger = 3f;
	public Transform tracker = null;
	public GameObject marker = null;
	public GUIText text = null;

	[SerializeField]
	float lastUpdate = 0;
	[SerializeField]
	float highest = 0;

	// Use this for initialization
	void Start () {
		highest = tracker.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (lastUpdate > 0 && Time.time > lastUpdate + linger) {
			highest = 0;
			lastUpdate = 0;
		}
		if (tracker.position.y > highest) {
			highest = tracker.position.y;
			lastUpdate = Time.time;
			text.text = string.Format("{0} meters high", highest );
			marker.transform.position = tracker.transform.position;
		}
	}
}
