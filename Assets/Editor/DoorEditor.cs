using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof( Door))]
public class DoorEditor : Editor {
	SerializedProperty openedPosition;

	public void OnEnable(){
		openedPosition = serializedObject.FindProperty ("openedPosition");
	}
	public void OnSceneGUI(){
		serializedObject.Update ();

		Door door = (Door)target;


		Handles.color = Color.green;
		Vector3 openedVec3 = openedPosition.vector3Value;

		openedPosition.vector3Value = Vector3.Scale (door.transform.InverseTransformPoint (Handles.FreeMoveHandle (
			door.transform.TransformPoint (openedVec3), 
			Quaternion.identity, 
			0.05f * HandleUtility.GetHandleSize (door.transform.TransformPoint (openedVec3)),
			Vector3.one * 0.1f, Handles.DotCap)),
		                                               new Vector3 (0, 1, 0));
		Handles.DrawDottedLine (door.transform.TransformPoint (openedVec3), door.transform.position, 20f );

		serializedObject.ApplyModifiedProperties ();

	}

}
