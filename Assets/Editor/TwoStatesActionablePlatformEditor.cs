using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TwoStatesActionablePlatform))]
public class TwoStatesActionablePlatformEditor : Editor {
	SerializedProperty pointB_SP;
	SerializedProperty speed_SP;
	SerializedProperty accelerationDistance_SP;
	SerializedProperty decelerationDistance_SP;
	public void OnEnable(){
		pointB_SP = serializedObject.FindProperty ("pointB");
		speed_SP = serializedObject.FindProperty ("speed");
		accelerationDistance_SP = serializedObject.FindProperty ("accelerationDistance");
		decelerationDistance_SP = serializedObject.FindProperty ("decelerationDistance");;
	}

	public void OnSceneGUI(){
		serializedObject.Update ();
		TwoStatesActionablePlatform platform = (TwoStatesActionablePlatform)target;
		Vector3 pointA = platform.transform.position;
		//-------------------------------------------------------------------
		//PointB handles and line
		Vector3 pointB = platform.transform.TransformPoint( pointB_SP.vector3Value);
		Handles.color = Color.blue;
		pointB = Handles.FreeMoveHandle (
			pointB,
			Quaternion.identity,
			HandleUtility.GetHandleSize (pointB) * 0.08f,
			Vector3.one * 0.1f,
			Handles.DotCap);

		pointB_SP.vector3Value = platform.transform.InverseTransformPoint (pointB);
		Handles.DrawDottedLine (pointA, pointB, 20f);

		//------------------------------------------------------------------
		//SPEED
		float speed = speed_SP.floatValue;
		float speed_to_screen = 0.25f;

		//ACCELERATION
		float accelerationDistance = accelerationDistance_SP.floatValue;
		Vector3 acc_A = Vector3.Lerp (pointA, pointB, accelerationDistance / Vector3.Distance (pointA, pointB) );
		Color c_acc = Color.green;
		c_acc.a = 0.8f;
		Handles.color = c_acc;
		Vector3 old_acc_A = acc_A;

		acc_A = Handles.FreeMoveHandle (
			acc_A,
			Quaternion.identity,
			HandleUtility.GetHandleSize (acc_A) * 0.05f,
			Vector3.one * 0.1f,
			Handles.DotCap);

		acc_A = HandleUtility.ProjectPointLine (acc_A, pointA, pointB);
		accelerationDistance_SP.floatValue = Vector3.Distance (pointA, acc_A);

		Vector3 acc_B = Vector3.Cross (pointB - pointA, Vector3.forward).normalized;
		acc_B *= speed * speed_to_screen;
		acc_B += acc_A;
		c_acc.a = 0.10f;
		Handles.color = c_acc;
		Handles.DrawAAConvexPolygon(new Vector3[]{pointA, acc_A, acc_B});




		//DECELERATION
		float decelerationDistance = decelerationDistance_SP.floatValue;
		Vector3 dec_A = Vector3.Lerp (pointB, pointA, decelerationDistance / Vector3.Distance (pointA, pointB) );
		Color c_dec = Color.red;
		Handles.color = c_dec;
		dec_A = Handles.FreeMoveHandle (
			dec_A,
			Quaternion.identity,
			HandleUtility.GetHandleSize (dec_A) * 0.05f,
			Vector3.one * 0.1f,
			Handles.DotCap);
		dec_A = HandleUtility.ProjectPointLine (dec_A, pointA, pointB);
		decelerationDistance_SP.floatValue = Vector3.Distance (pointB, dec_A);

		Vector3 dec_B = Vector3.Cross (pointB - pointA, Vector3.forward).normalized;
		dec_B *= speed * speed_to_screen;
		dec_B += dec_A;
		c_dec.a = 0.10f;
		Handles.color = c_dec;
		Handles.DrawAAConvexPolygon(new Vector3[]{pointB, dec_A, dec_B});

		//SPEED
		Color c_spe = Color.magenta;
		Handles.color = c_spe;
		Vector3 speed_B = (acc_B + dec_B) / 2f;
		speed_B = Handles.FreeMoveHandle (
			speed_B,
			Quaternion.identity,
			HandleUtility.GetHandleSize (speed_B) * 0.05f,
			Vector3.one * 0.1f,
			Handles.DotCap);

		speed_SP.floatValue = HandleUtility.DistancePointLine (speed_B, pointA, pointB)/ speed_to_screen;
		c_spe.a = 0.10f;
		Handles.color = c_spe;
		Handles.DrawAAConvexPolygon(new Vector3[]{acc_A, acc_B, dec_B, dec_A});



		serializedObject.ApplyModifiedProperties ();
	}

}
