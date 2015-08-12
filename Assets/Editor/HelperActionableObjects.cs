using System.Collections;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(WeigthButton))]
public class HelperActionableObjects : Editor {

    public void OnSceneGUI()
    {
        WeigthButton myTarget = (WeigthButton)target;
        foreach (GameObject actionableObject in myTarget.actionableObjects)
        {
            if (actionableObject == null)
            {
                string message = String.Format("Yo mama told you to link that door to that button. Never forget again, son", myTarget.name );
                Debug.LogError(message);
                Debug.Break();
                return;
            }
            IActionableElement actionableElement = actionableObject.GetComponent<IActionableElement>();
            if (actionableElement == null)
            {
                string message = String.Format("Yo mama told you the {0} is a invalid 'door'. Never forget again, son.", actionableObject.name);
                Debug.LogError(message);
                Debug.Break();
                return;
            } 
            Handles.color = Color.green;
            Handles.DrawLine(myTarget.transform.position, actionableObject.transform.position);
        }
        
    }
}