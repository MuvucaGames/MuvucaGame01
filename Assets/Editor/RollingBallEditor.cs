using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RollingBall))]
public class RollingBallEditor : Editor
{
    SerializedProperty force_SP;
    public void OnEnable()
    {
        force_SP = serializedObject.FindProperty("force");
    }
    public void OnSceneGUI()
    {
        serializedObject.Update();
        RollingBall throwObj = (RollingBall)target;
        Vector3 pointA = throwObj.transform.position;
        //-------------------------------------------------------------------
        //PointB handles and line
        Vector3 pointB = throwObj.transform.TransformPoint(force_SP.vector3Value/-100);
        Handles.color = Color.blue;
        pointB = Handles.FreeMoveHandle(
            pointB,
            Quaternion.identity,
            HandleUtility.GetHandleSize(pointB) * 0.08f,
            Vector3.one * 0.1f,
            Handles.DotCap);

        force_SP.vector3Value = throwObj.transform.InverseTransformPoint(pointB)*-100;
        Handles.DrawDottedLine(pointA, pointB, 20f);
        serializedObject.ApplyModifiedProperties();

    }
}