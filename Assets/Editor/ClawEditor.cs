using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ClawController))]
[ExecuteInEditMode]
public class ClawEditor : Editor {
    SerializedProperty minHeight_;
    SerializedProperty horizontalVelocity_;
    SerializedProperty verticalVelocity_;
    SerializedProperty allowDrawing_;

	public void OnEnable()
    {
		minHeight_ = serializedObject.FindProperty("minHeight");
        horizontalVelocity_ = serializedObject.FindProperty("horizontalVelocity");
        verticalVelocity_ = serializedObject.FindProperty("verticalVelocity");
        allowDrawing_ = serializedObject.FindProperty("allowDrawing");
	}

    public override void OnInspectorGUI()
    {
		serializedObject.Update ();
        ClawController clawController = target as ClawController;
        minHeight_.floatValue = EditorGUILayout.FloatField("Minimum Height: ", minHeight_.floatValue);
        verticalVelocity_.floatValue = EditorGUILayout.Slider("Vertical Velocity: ", verticalVelocity_.floatValue, 0f, 10f);
        horizontalVelocity_.floatValue = EditorGUILayout.Slider("Horizontal Velocity: ", horizontalVelocity_.floatValue, 0f, 10f);
        allowDrawing_.boolValue = EditorGUILayout.Toggle("Allow Drawing: ", allowDrawing_.boolValue);
		serializedObject.ApplyModifiedProperties();
    }
}
