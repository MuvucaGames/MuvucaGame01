using System.Collections;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(WeigthButton))]
public class HelperActionableObjects : Editor {

    public void OnSceneGUI()
    {
        WeigthButton myTarget = (WeigthButton)target;
        foreach (ActionableElement actionableElement in myTarget.actionableObjects)
        {
            if (actionableElement == null)
            {
                string message = String.Format("Yo mama told you to link that door to that button. Never forget again, son", myTarget.name );
                Debug.LogError(message);
                Debug.Break();
                return;
            }
            Handles.color = Color.green;
            Handles.DrawLine(myTarget.transform.position, actionableElement.transform.position);
        }
        
    }
}