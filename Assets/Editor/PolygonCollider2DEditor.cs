using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PolygonCollider2D))]
public class PolygonCollider2DEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		PolygonCollider2D collider = (PolygonCollider2D)target;
		Vector2[] points = collider.points;
		for (int i = 0; i < points.Length; i++)
		{
			points[i] = EditorGUILayout.Vector2Field(i.ToString(), points[i]);
		}
		collider.points = points;
		EditorUtility.SetDirty(target);
	}
}
