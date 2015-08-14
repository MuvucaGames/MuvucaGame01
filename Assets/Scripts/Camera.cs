using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {


	[SerializeField] private float cameraSpeed = 0.3f;

	[SerializeField]private Hero heroStrong = null, heroFast = null;
	private Vector3 m_newPosition;
	private Vector3 velocity = Vector3.zero;

	void Awake () {
		if(heroStrong == null || heroFast == null)
			throw new UnityException ("Missing heroes in Camera Control");

		if (heroStrong.IsActive) {
			m_newPosition = new Vector3(heroStrong.transform.position.x, heroStrong.transform.position.y, -1f);
		} else {
			m_newPosition = new Vector3(heroFast.transform.position.x, heroFast.transform.position.y, -1f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (heroStrong.IsActive) {
			m_newPosition = new Vector3(heroStrong.transform.position.x, heroStrong.transform.position.y, -1f);
		} else {
			m_newPosition = new Vector3(heroFast.transform.position.x, heroFast.transform.position.y, -1f);
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
