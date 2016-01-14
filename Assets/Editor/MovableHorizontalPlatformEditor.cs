using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MovableHorizontalPlatform))]
public class MovableHorizontalPlatformEditor : Editor {
	SerializedProperty pointA_SP;
	SerializedProperty pointB_SP;
	SerializedProperty speed_SP;

	public void OnEnable(){
		pointA_SP = serializedObject.FindProperty ("pointA");
		pointB_SP = serializedObject.FindProperty ("pointB");
		speed_SP = serializedObject.FindProperty ("speed");
	}
	public void OnSceneGUI(){
		serializedObject.Update ();
		MovableHorizontalPlatform platform = (MovableHorizontalPlatform)target;
		Vector3 pointA = platform.transform.TransformPoint( new Vector3(pointA_SP.vector3Value.x, 0, 0));
//		Vector3 pointA = platform.transform.TransformPoint( pointA_SP.vector3Value);
		Handles.color = Color.blue;
		pointA = Handles.FreeMoveHandle (
			pointA,
			Quaternion.identity,
			HandleUtility.GetHandleSize (pointA) * 0.08f,
			Vector3.one * 0.1f,
			Handles.DotCap);
		//-------------------------------------------------------------------
		//PointB handles and line
		Vector3 pointB = platform.transform.TransformPoint( new Vector3(pointB_SP.vector3Value.x, 0, 0));
//		Vector3 pointB = platform.transform.TransformPoint( pointB_SP.vector3Value);
		Handles.color = Color.blue;
		pointB = Handles.FreeMoveHandle (
			pointB,
			Quaternion.identity,
			HandleUtility.GetHandleSize (pointB) * 0.08f,
			Vector3.one * 0.1f,
			Handles.DotCap);
		
		pointA_SP.vector3Value = platform.transform.InverseTransformPoint (pointA);
		pointB_SP.vector3Value = platform.transform.InverseTransformPoint (pointB);
		Handles.DrawDottedLine (pointA, pointB, 20f);
		
		//------------------------------------------------------------------
		//SPEED
		float speed = speed_SP.floatValue;
		float speed_to_screen = 0.25f;

		serializedObject.ApplyModifiedProperties ();

	}

}
