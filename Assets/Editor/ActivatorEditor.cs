using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

[CustomEditor (typeof(Activator), true)]
public class ActivatorEditor : Editor {

	SerializedProperty actionableElements_sp;
	public void OnEnable(){
		actionableElements_sp = serializedObject.FindProperty ("actionableElements");

	}

	public void OnSceneGUI(){
		serializedObject.Update ();
		Activator activator = (Activator)target;
		RemoveRepeatedElements ();


		Handles.color = new Color (0.9f, 0.1f, 0.1f, 0.3f);
		Handles.DrawSolidDisc(activator.transform.position, Vector3.forward, HandleUtility.GetHandleSize(activator.transform.position)*0.1f); 

		//Draw Lines And Arrows
		Handles.color = new Color (0.68f, 0.48f, 0.87f, 0.5f);
		foreach (ActionableElement act_elem in activator.ActionableElements) {
			if(act_elem != null){
				//Handles.DrawLine(activator.transform.position, act_elem.transform.position);
				Handles.ArrowCap(0, activator.transform.position, Quaternion.FromToRotation(Vector3.forward, act_elem.transform.position - activator.transform.position), Vector3.Distance(activator.transform.position, act_elem.transform.position)*0.85f) ;
			}
		}


		ActionableElement [] allActionablesOnScene = FindObjectsOfType<ActionableElement> ();
		Activator [] allActivatorsOnScene = FindObjectsOfType<Activator> ();
		foreach (ActionableElement act_elem in allActionablesOnScene) {
			if(act_elem.gameObject.Equals(activator.gameObject))
				continue;

			Handles.BeginGUI();
			Vector2 size = new Vector2(100,100);

			GUILayout.BeginArea(new Rect(HandleUtility.WorldToGUIPoint(act_elem.transform.position) - (size/2f), size));
			GUILayout.Label(act_elem.name.ToString());

			int count = 0;
			foreach(Activator activator_i in allActivatorsOnScene){
				if(activator_i!= activator && activator_i.ActionableElements.Contains(act_elem)){
					count ++;
				}
			}
			if(count > 0){
				GUILayout.TextArea("Linked to "  + count + " other Activator(s)");
			}


			if(!activator.ActionableElements.Contains( act_elem)){
				if(GUILayout.Button("LINK")){
					actionableElements_sp.arraySize++;
					actionableElements_sp.GetArrayElementAtIndex(actionableElements_sp.arraySize - 1).objectReferenceValue = act_elem;
				}
			}else{
				if(GUILayout.Button("UNLINK")){
					for(int i = 0; i<actionableElements_sp.arraySize; i++){
						if(actionableElements_sp.GetArrayElementAtIndex(i).objectReferenceValue == act_elem){
							actionableElements_sp.DeleteArrayElementAtIndex(i);
						}
                        if (actionableElements_sp.GetArrayElementAtIndex(i).objectReferenceValue == null)
                        {
                            actionableElements_sp.DeleteArrayElementAtIndex(i);
                        }
                    }
				}
			}

			GUILayout.EndArea();
			Handles.EndGUI();
		}



		serializedObject.ApplyModifiedProperties ();
	}

	public void RemoveRepeatedElements(){
		Activator activator = (Activator)target;
		activator.ActionableElements = activator.ActionableElements.Distinct ().ToList ();
	}

}
