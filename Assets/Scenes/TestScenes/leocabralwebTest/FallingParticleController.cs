using UnityEngine;
using System.Collections;

public class FallingParticleController : MonoBehaviour {

	public int yLimit = 0;
	public float speed = 1.5f;
	public GameObject targetObject;
	private GameObject target;

	private float startX;
	private float startY;

	void Start () {
		startX = transform.position.x;
		startY = transform.position.y;
		SpawnObject ();
	}

	void SpawnObject() {
		target = Instantiate (targetObject) as GameObject;
		target.SetActive (true);
		target.transform.position = transform.position;
	}

	void resetPosition() {
		target.transform.position = new Vector3 (startX, startY, 0);
	}

	void Fall() {
		if (target.transform.position.y > yLimit) {
			float yIncrement = (float)(-speed * Time.deltaTime);
			target.transform.Translate (0, yIncrement, 0);
		} else {
			resetPosition ();
		}
	}

	void Update () {
		Fall ();
	}
}