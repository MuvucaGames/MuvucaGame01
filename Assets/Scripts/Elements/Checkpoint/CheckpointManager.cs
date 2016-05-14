using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointManager : MonoBehaviour {
	public GameObject heroStrong;
	public GameObject heroFast;
	private static int lastCheckpoint = 0;
	private static int lastLevel;

	void Awake()
	{
		if (PlayerChangedLevel ()) 
		{
			lastCheckpoint = 0;
			lastLevel = Application.loadedLevel;
		}

		if (lastCheckpoint > 0) 
		{
			//GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
			Checkpoint[] checkpoints = GameObject.FindObjectsOfType<Checkpoint> ();
			foreach (Checkpoint checkpoint in checkpoints)
			{
				
				if (checkpoint.checkpointOrder == lastCheckpoint)
				{
					PlaceHeroes(checkpoint);
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

	public void ResetCheckpoins()
	{
		lastCheckpoint = 0;
	}

	private void PlaceHeroes(Checkpoint checkpoint)
	{
		heroStrong.transform.position = checkpoint.HeroStrongTransform.position;
		heroFast.transform.position = checkpoint.HeroFastTransform.position;
	}

	private bool PlayerChangedLevel()
	{
		return lastLevel != Application.loadedLevel;
	}
}
