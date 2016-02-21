using UnityEngine;
using System.Collections;

public class SceneSogro : Scene {
	private bool RunsOnce = false;
	private uint action = 0;

	public override void SceneAwake()
	{
	}

	public override void SceneLoop()
	{
		if (!RunsOnce) {
			RunsOnce = true;
			switch (action) {
			case 0:
				Balloon _ballonInstance = Instantiate (dialog.balloonPrefab);
				_ballonInstance.Init (dialog.senteces [0], AdvanceInScene);
				break;
			case 1:
				_ballonInstance = Instantiate (dialog.balloonPrefab);
				_ballonInstance.Init (dialog.senteces [1], AdvanceInScene);
				break;
			default:
				EndScene ();
				break;
			}
		}
	}

	private void AdvanceInScene()
	{
		RunsOnce = false;
		action++;
	}

}
