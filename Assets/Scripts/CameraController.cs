using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private static CameraController instance;
    public static CameraController Instance
    {
        get
        {
            Debug.AssertFormat(instance != null, "Instance of CameraController is not initialized");
            return instance;
        }
    }

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

    public Controllable focusableObject = null;

	void Start () {
        instance = this;
        myCamera = Camera.main;
        float cameraZ = myCamera.transform.position.z;

		if (heroStrong == null || heroFast == null) {
			heroStrong = FindObjectOfType<HeroStrong>();
			heroFast = FindObjectOfType<HeroFast>();
			if (heroStrong == null || heroFast == null)
				throw new UnityException ("Missing heroes in Camera Control");
		}

        if (heroStrong.IsActive)
        {
            focusableObject = heroStrong;
        }
        else if (heroFast.IsActive)
        {
            focusableObject = heroFast;
        }
	}
	
	// Update is called once per frame
	void Update () {
        float cameraZ = myCamera.transform.position.z;

        if (!m_isOnCutscene && focusableObject != null) {
            m_newPosition = new Vector3 (focusableObject.transform.position.x, 
                                         focusableObject.transform.position.y, cameraZ);

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
    public void SetCameraControl(Controllable obj)
    {
        focusableObject = obj;
    }
}
