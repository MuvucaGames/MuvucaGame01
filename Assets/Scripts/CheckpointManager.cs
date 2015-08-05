using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointManager : MonoBehaviour {
	public GameObject heroStrong;
	private static int lastCheckpoint = 0;

	void Awake()
	{
		if (lastCheckpoint > 0) 
		{
			GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
			foreach (GameObject o in checkpoints)
			{
				Checkpoint script = o.GetComponent<Checkpoint>();
				if (script.checkpointOrder == lastCheckpoint)
				{
					heroStrong.transform.position = script.heroStrongPosition;
					break;
				}
			}
		}
	}

	public void RegisterCheckpoint(Checkpoint checkpoint)
	{
		if (checkpoint.checkpointOrder > lastCheckpoint) 
		{
			lastCheckpoint = checkpoint.checkpointOrder;
		}
	}

}
