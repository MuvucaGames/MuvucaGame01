using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, IActionableElement {
	private Vector3 closedPosition;
	private Vector3 openedPosition;
	void Awake()
	{
		closedPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		openedPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 4, gameObject.transform.position.z);
	}

	public void Activate()
	{
		gameObject.transform.position = openedPosition;
	}

	public void Deactivate()
	{
		gameObject.transform.position = closedPosition;
	}

}
