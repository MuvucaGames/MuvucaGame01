using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MoveAtoB))]
public class BirdEditor : Editor
{
    SerializedProperty pointA_SP; 
    SerializedProperty pointB_SP;
    SerializedProperty speed_SP;

    public void OnEnable()
    {
        pointA_SP = serializedObject.FindProperty("pointA"); 
        pointB_SP = serializedObject.FindProperty("pointB");
        speed_SP = serializedObject.FindProperty("speed");
    }
    public void OnSceneGUI()
    {
        serializedObject.Update();
        MoveAtoB platform = (MoveAtoB)target;   
        Vector3 pointA = platform.transform.TransformPoint(new Vector3(pointA_SP.vector3Value.x, 0, 0));     

        Handles.color = Color.blue;       
   
        Vector3 pointB = platform.transform.TransformPoint(new Vector3(pointB_SP.vector3Value.x, 0, 0));

        Handles.color = Color.blue;
        pointB = Handles.FreeMoveHandle(
            pointB,
            Quaternion.identity,
            HandleUtility.GetHandleSize(pointB) * 0.08f,
            Vector3.one * 0.1f,
            Handles.DotCap);
        
        pointB_SP.vector3Value = platform.transform.InverseTransformPoint(pointB);
        Handles.DrawDottedLine(pointA, pointB, 20f);

        float speed = speed_SP.floatValue;   
        serializedObject.ApplyModifiedProperties();
    }
}

