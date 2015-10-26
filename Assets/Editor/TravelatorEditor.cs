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
		Transform pointB = travelator.transform.FindChild("PointB");
		pointA.position = EditorGUILayout.Vector3Field ("PointA", pointA.position);
		pointB.position = EditorGUILayout.Vector3Field ("PointB", pointB.position);

		if (GUI.changed) {
			UpdateGUI(travelator, pointA, pointB);
		}

		travelator.GetComponentInChildren<SurfaceEffector2D> ().speed = EditorGUILayout.Slider ("Speed", travelator.GetComponentInChildren<SurfaceEffector2D> ().speed, -10f, 10);

	}


	public void OnSceneGUI(){
		Travelator travelator = (Travelator)target;
		Transform pointA = travelator.transform.FindChild("PointA");
		Transform pointB = travelator.transform.FindChild("PointB");


		pointA.position = Handles.PositionHandle (pointA.position, Quaternion.identity);
		pointB.position = Handles.PositionHandle (pointB.position, Quaternion.identity);


		if (GUI.changed) {
			UpdateGUI(travelator, pointA, pointB);

		}
	}

	private void UpdateGUI(Travelator travelator, Transform pointA, Transform pointB){
		Transform belt = travelator.transform.FindChild ("Belt");
		Vector3 difP = (pointA.position + pointB.position)/2f - travelator.transform.position;
		travelator.transform.position = (pointA.position + pointB.position)/2f;
		pointA.position -= difP;
		pointB.position -= difP;
		
		Vector3 newRotation = new Vector3(0,0,Vector3.Angle(pointA.position, pointB.position));
		belt.localRotation = Quaternion.FromToRotation(Vector3.right, pointA.position - pointB.position);
		
		Vector3 newScale = new Vector3(Vector3.Distance(pointA.position, pointB.position) /2f, 1, 1);
		belt.transform.localScale = newScale;
	}

}
