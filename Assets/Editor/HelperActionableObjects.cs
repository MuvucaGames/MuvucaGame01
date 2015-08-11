using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WeigthButton))]
public class HelperActionableObjects : Editor {

    public void OnSceneGUI()
    {
        WeigthButton myTarget = (WeigthButton)target;
        foreach (GameObject actionableObject in myTarget.actionableObjects)
        {

            Handles.color = Color.green;
            Handles.DrawLine(myTarget.transform.position, actionableObject.transform.position);
        }
        
    }
}