using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private Camera myCamera;

	[SerializeField] private float cameraSpeed = 0.3f;

	[SerializeField]private Hero heroStrong = null, heroFast = null;
	private Vector3 m_newPosition;
	private Vector3 velocity = Vector3.zero;

	public bool m_isOnCutscene = false;
	public bool IsOnActive{
		get { return m_isOnCutscene; }
		protected set { m_isOnCutscene = value; }
	}
    public GameObject interactiveFocusableObject = null;

	void Awake () {
        myCamera = Camera.main;
        float cameraZ = myCamera.transform.position.z;

		if (heroStrong == null || heroFast == null) {
			heroStrong = FindObjectOfType<HeroStrong>();
			heroFast = FindObjectOfType<HeroFast>();
			if (heroStrong == null || heroFast == null)
				throw new UnityException ("Missing heroes in Camera Control");
		}


		if (heroStrong.IsActive) {
			m_newPosition = new Vector3(heroStrong.transform.position.x, heroStrong.transform.position.y, cameraZ);
		} else {
			m_newPosition = new Vector3(heroFast.transform.position.x, heroFast.transform.position.y, cameraZ);
		}
	}
	
	// Update is called once per frame
	void Update () {
        float cameraZ = myCamera.transform.position.z;


        if (!m_isOnCutscene) {
            if (heroStrong.IsActive) {
                m_newPosition = new Vector3 (heroStrong.transform.position.x, heroStrong.transform.position.y, cameraZ);
            } else if (heroFast.IsActive) {
                m_newPosition = new Vector3 (heroFast.transform.position.x, heroFast.transform.position.y, cameraZ);
            } else {
                m_newPosition = new Vector3 (interactiveFocusableObject.transform.position.x, interactiveFocusableObject.transform.position.y, cameraZ);
            }

			if (Input.GetKey (KeyCode.DownArrow)) {
				m_newPosition += new Vector3 (0, -1.5f);
			}

			if (heroStrong.OnAir || heroFast.OnAir) {
				m_newPosition += new Vector3 (0, 1.5f);
			}
			myCamera.transform.position = Vector3.SmoothDamp (myCamera.transform.position, m_newPosition, ref velocity, cameraSpeed);
			//transform.position = m_newPosition;
		}
	}

	public Camera getCamera() {return myCamera;}
}
