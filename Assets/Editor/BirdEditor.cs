using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MoveAtoB))]
public class BirdEditor : Editor
{
    SerializedProperty pointA_SP; 
    SerializedProperty pointB_SP;   

    public void OnEnable()
    {
        pointA_SP = serializedObject.FindProperty("pointA"); 
        pointB_SP = serializedObject.FindProperty("pointB");
    }
    public void OnSceneGUI()
    {
        serializedObject.Update();
        MoveAtoB platform = (MoveAtoB)target;
        Vector3 pointA = platform.transform.TransformPoint(pointA_SP.vector3Value);
        Handles.color = Color.blue;       
   
        Vector3 pointB = platform.transform.TransformPoint(pointB_SP.vector3Value);

        Handles.color = Color.blue;
        pointB = Handles.FreeMoveHandle(
            pointB,
            Quaternion.identity,
            HandleUtility.GetHandleSize(pointB) * 0.08f,
            Vector3.one * 0.1f,
            Handles.DotCap);
        
        pointB_SP.vector3Value = platform.transform.InverseTransformPoint(pointB);
        Handles.DrawDottedLine(pointA, pointB, 20f);
        
        serializedObject.ApplyModifiedProperties();
    }
}

