using UnityEngine;
using System.Collections;

public class Door : ActionableElement {
	private Vector3 closedPosition;
	private Vector3 openedPosition;
    // Check door closed or opened: if checked, then its opened, if unchecked, then closed
    public bool isDoorOpened;

    void Awake()
	{
		closedPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		openedPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 4, gameObject.transform.position.z);
	}

	public override void Activate()
	{
		gameObject.transform.position = openedPosition;
		gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
        SoundManager.Instance.SendMessage("PlaySFXOpenGate");
        isDoorOpened = true;
	}

	public override void Deactivate()
	{
		gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        SoundManager.Instance.SendMessage("PlaySFXCloseGate");
        isDoorOpened = false;
	}
}
