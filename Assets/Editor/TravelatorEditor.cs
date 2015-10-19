using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (Travelator))]
public class TravelatorEditor : Editor {


	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		Travelator travelator = (Travelator)target;

		Transform pointA = travelator.transform.FindChild("PointA");
	}

	public void OnSceneGUI(){
		Travelator travelator = (Travelator)target;
		
		Transform pointA = travelator.transform.FindChild("PointA");
		Transform pointB = travelator.transform.FindChild("PointB");
		
		Debug.Log (pointA.position);
		Handles.color = Color.blue;
		Handles.DrawLine (pointA.position, pointB.position);
	}

}
