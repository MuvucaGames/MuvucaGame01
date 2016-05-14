using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour {
	public float maxHeightMultiplier = 2f;

	void OnTriggerEnter2D(Collider2D other) {
		Hero hero = other.GetComponentInParent<Hero> ();

		if (hero != null && hero.transform.position.y > transform.position.y) {
			hero.Jump(maxHeightMultiplier);
		}
	}
}