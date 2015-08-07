using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

	void OnTriggerEnter2D()
	{
		//TO DO use Game Controller to reload
		Application.LoadLevel(Application.loadedLevel);
	}
}
