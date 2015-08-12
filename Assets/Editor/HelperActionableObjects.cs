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
            IActionableElement actionableElement = actionableObject.GetComponent<IActionableElement>();
            if (actionableElement == null)
            {
                string message = String.Format("O objeto {0} não implementa a interface IActionableElement.", actionableObject.name);
                Debug.LogError(message);
                Debug.Break();
            } 
            Handles.color = Color.green;
            Handles.DrawLine(myTarget.transform.position, actionableObject.transform.position);
        }
        
    }
}