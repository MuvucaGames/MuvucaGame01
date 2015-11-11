using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Checkpoint))]
public class CheckPointEditor : Editor {

	Transform heroFast;
	Transform heroStrong;

	public void OnEnable(){
		Checkpoint checkpoint = (Checkpoint)target;
		heroFast = checkpoint.transform.FindChild ("FastHeroRestartPosition");
		heroStrong = checkpoint.transform.FindChild ("StrongHeroRestartPosition");

		heroFast.gameObject.SetActive (true);
		heroStrong.gameObject.SetActive (true);
	}

	public void OnDisable(){
		heroFast.gameObject.SetActive (false);
		heroStrong.gameObject.SetActive (false);
	}

	public override void OnInspectorGUI ()
	{
		heroFast.position = EditorGUILayout.Vector3Field ("Hero Fast", heroFast.position);
		heroStrong.position = EditorGUILayout.Vector3Field ("Hero Strong", heroStrong.position);
		base.OnInspectorGUI ();
	}

	 void OnSceneGUI(){
		serializedObject.Update ();
		Checkpoint checkpoint = (Checkpoint)target;

		Handles.matrix = checkpoint.transform.localToWorldMatrix;

		//HANDLES FOR THE RESPAWN POSITION
		Color spawn_color = new Color (0.8f, 0.2f, 0.2f, 0.6f);
		Handles.color = spawn_color;
		heroFast.localPosition = Handles.FreeMoveHandle (
			heroFast.localPosition,
			Quaternion.identity,
			HandleUtility.GetHandleSize(heroFast.localPosition) * 0.3f,
			Vector3.one * 0.1f,
			Handles.SphereCap);
		Handles.DrawLine (Vector3.zero, heroFast.transform.localPosition);

		heroStrong.localPosition = Handles.FreeMoveHandle (
			heroStrong.localPosition,
			Quaternion.identity,
			HandleUtility.GetHandleSize(heroStrong.localPosition) * 0.3f,
			Vector3.one * 0.1f,
			Handles.SphereCap);
		Handles.DrawLine (Vector3.zero, heroStrong.transform.localPosition);

		spawn_color.b = 0.8f;
		spawn_color.r = 0.5f;
		Handles.matrix = Matrix4x4.identity;
		Handles.color = spawn_color;
		checkpoint.transform.localPosition = Handles.FreeMoveHandle (
			checkpoint.transform.localPosition,
			Quaternion.identity,
			HandleUtility.GetHandleSize(checkpoint.transform.localPosition) * 0.3f,
			Vector3.one * 0.1f,
			Handles.ConeCap);
		Handles.matrix = checkpoint.transform.localToWorldMatrix;

		//HANDLES FOR THE BOX COLLIDER TRIGGER
		//TODO There must be an easier way...
		BoxCollider2D boxCollider2D = checkpoint.GetComponent<BoxCollider2D> ();
		float dotSize = 0.08f;

		Color bc2d_color = new Color (0.1f, 0.8f, 0.1f, 0.8f);
		Handles.color = bc2d_color;


		Bounds bounds = boxCollider2D.bounds;
		bounds.center = checkpoint.transform.InverseTransformPoint (bounds.center);

		//------UP LEFT
		Vector3 up_left = new Vector3(bounds.min.x, bounds.max.y);
		up_left = Handles.FreeMoveHandle(
			up_left,
			Quaternion.identity,
			HandleUtility.GetHandleSize (up_left) * dotSize,
			Vector3.one * 0.1f,
			Handles.DotCap);
		if(GUI.changed)
			bounds = ResetBounds (up_left.x, null, null, up_left.y ,bounds);

		//------UP RIGHT
		Vector3 up_right = new Vector3(bounds.max.x, bounds.max.y);
		up_right = Handles.FreeMoveHandle(
			up_right,
			Quaternion.identity,
			HandleUtility.GetHandleSize (up_right) * dotSize,
			Vector3.one * 0.1f,
			Handles.DotCap);
		if(GUI.changed)
			bounds = ResetBounds (null, null, up_right.x, up_right.y ,bounds);

		//------DOWN LEFT
		Vector3 down_left = new Vector3(bounds.min.x, bounds.min.y);
		down_left = Handles.FreeMoveHandle(
			down_left,
			Quaternion.identity,
			HandleUtility.GetHandleSize (down_left) * dotSize,
			Vector3.one * 0.1f,
			Handles.DotCap);
		if(GUI.changed)
			bounds = ResetBounds (down_left.x, down_left.y, null, null ,bounds);

		//------DOWN RIGHT
		Vector3 down_right = new Vector3(bounds.max.x, bounds.min.y);
		down_right = Handles.FreeMoveHandle(
			down_right,
			Quaternion.identity,
			HandleUtility.GetHandleSize (down_right) * dotSize,
			Vector3.one * 0.1f,
			Handles.DotCap);
		if(GUI.changed)
			bounds = ResetBounds (null, down_right.y, down_right.x, null ,bounds);


		if (GUI.changed) {
			boxCollider2D.offset = bounds.center;
			boxCollider2D.size = bounds.size;
		}


		serializedObject.ApplyModifiedProperties ();
	}

	private Bounds ResetBounds(float? xmin, float? ymin, float? xmax, float? ymax, Bounds b){
		b.max = new Vector3 (xmax.HasValue ? xmax.Value : b.max.x , ymax.HasValue ? ymax.Value : b.max.y);
		b.min = new Vector3 (xmin.HasValue ? xmin.Value : b.min.x , ymin.HasValue ? ymin.Value : b.min.y);

		return b;
	}
}
