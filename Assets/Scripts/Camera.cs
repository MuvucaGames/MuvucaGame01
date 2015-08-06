using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	[SerializeField] private Transform strongTransform;
	[SerializeField] private Transform fastTransform;
	[SerializeField] private float cameraSpeed = 0.3f;

	private Hero heroStrong, heroFast;
	private Vector3 m_newPosition;
	private Vector3 velocity = Vector3.zero;

	
	void Awake () {
		heroStrong = GameObject.Find ("HeroStrong").GetComponent<Hero> ();
		heroFast = GameObject.Find ("HeroFast").GetComponent<Hero> ();

		if (heroStrong.IsActive) {
			m_newPosition = new Vector3(strongTransform.transform.position.x, strongTransform.transform.position.y, -1f);
		} else {
			m_newPosition = new Vector3(fastTransform.transform.position.x, fastTransform.transform.position.y, -1f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (heroStrong.IsActive) {
			m_newPosition = new Vector3(strongTransform.transform.position.x, strongTransform.transform.position.y, -1f);
		} else {
			m_newPosition = new Vector3(fastTransform.transform.position.x, fastTransform.transform.position.y, -1f);
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			m_newPosition += new Vector3(0, -1.5f);
		}

		if (heroStrong.OnAir || heroFast.OnAir) {
			m_newPosition += new Vector3(0, 1.5f);
		}

		transform.position = Vector3.SmoothDamp (transform.position, m_newPosition, ref velocity, cameraSpeed);
		//transform.position = m_newPosition;
	}
}
