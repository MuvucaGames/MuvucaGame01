using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

	void OnTriggerEnter2D()
	{
        SoundManager.Instance.SendMessage("PlaySFXWater");
		//TO DO use Game Controller to reload
        SoundManager.Instance.SendMessage("PlaySFXReset"); // -- Need change
		Application.LoadLevel(Application.loadedLevel);
	}
}
