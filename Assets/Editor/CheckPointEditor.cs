using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Checkpoint))]
public class CheckPointEditor : Editor {



	public void OnSceneGUI(){



		serializedObject.Update ();
		Checkpoint checkpoint = (Checkpoint)target;


		//HANDLES FOR THE BOX COLLIDER TRIGGER
		//TODO There must be an easier way...
		BoxCollider2D boxCollider2D = checkpoint.GetComponent<BoxCollider2D> ();

		Color bc2d_color = new Color (0.1f, 0.8f, 0.1f, 0.8f);
		Handles.color = bc2d_color;

		Handles.matrix = checkpoint.transform.localToWorldMatrix;
		Bounds bounds = boxCollider2D.bounds;
		bounds.center = checkpoint.transform.InverseTransformPoint (bounds.center);

		//------UP LEFT
		Vector3 up_left = new Vector3(bounds.min.x, bounds.max.y);
		up_left = Handles.FreeMoveHandle(
			up_left,
			Quaternion.identity,
			HandleUtility.GetHandleSize (up_left) * 0.1f,
			Vector3.one * 0.1f,
			Handles.DotCap);
		if(GUI.changed)
			bounds = ResetBounds (up_left.x, null, null, up_left.y ,bounds);

		//------UP RIGHT
		Vector3 up_right = new Vector3(bounds.max.x, bounds.max.y);
		up_right = Handles.FreeMoveHandle(
			up_right,
			Quaternion.identity,
			HandleUtility.GetHandleSize (up_right) * 0.1f,
			Vector3.one * 0.1f,
			Handles.DotCap);
		if(GUI.changed)
			bounds = ResetBounds (null, null, up_right.x, up_right.y ,bounds);

		//------DOWN LEFT
		Vector3 down_left = new Vector3(bounds.min.x, bounds.min.y);
		down_left = Handles.FreeMoveHandle(
			down_left,
			Quaternion.identity,
			HandleUtility.GetHandleSize (down_left) * 0.1f,
			Vector3.one * 0.1f,
			Handles.DotCap);
		if(GUI.changed)
			bounds = ResetBounds (down_left.x, down_left.y, null, null ,bounds);

		//------DOWN RIGHT
		Vector3 down_right = new Vector3(bounds.max.x, bounds.min.y);
		down_right = Handles.FreeMoveHandle(
			down_right,
			Quaternion.identity,
			HandleUtility.GetHandleSize (down_right) * 0.1f,
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
