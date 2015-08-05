using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	public Vector3 heroStrongPosition;
	public GameObject manager;
	public int checkpointOrder;

	void OnTriggerEnter2D() {
		CheckpointManager script = manager.GetComponent<CheckpointManager> ();
		script.RegisterCheckpoint (this);
	}
}
