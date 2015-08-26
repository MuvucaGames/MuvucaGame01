using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeigthButtonWithPhysics : MonoBehaviour {

	[SerializeField] private SpringJoint2D springJoint2D = null;
	[SerializeField] private float activationWeight = 1f;
	[SerializeField]private Rigidbody2D selfWeight = null;
	[SerializeField] private List<ActionableElement> actionableElements = new List<ActionableElement>();

	private bool wasActivated = false;
	private List<float> lastMeasures = new List<float>();
	private float meanWeight = 0f;

	void FixedUpdate(){
		CalculateMeanWeight ();

		print (meanWeight);

		if (meanWeight >= activationWeight && !wasActivated) {
			wasActivated = true;
			foreach (ActionableElement actEle in actionableElements)
				actEle.Activate ();
		} else if (meanWeight < activationWeight && wasActivated) {
			wasActivated = false;
			foreach (ActionableElement actEle in actionableElements)
				actEle.Deactivate ();
		}
	}

	private void CalculateMeanWeight(){
		while (lastMeasures.Count>30) {
			lastMeasures.RemoveAt(0);
		}
		float reactionForce = springJoint2D.GetReactionForce (Time.deltaTime).y;
		lastMeasures.Add (reactionForce / Physics2D.gravity.y);

		float sum = 0f;
		foreach (float f in lastMeasures) {
			sum += f;
		}
		meanWeight =  sum / lastMeasures.Count;

		meanWeight = Mathf.Round (meanWeight * 100f) / 100f;

		meanWeight -= selfWeight.mass;

	}




}
