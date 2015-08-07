using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

	void OnTriggerEnter2D()
	{
        Game.ReloadLevel();
	}
}
