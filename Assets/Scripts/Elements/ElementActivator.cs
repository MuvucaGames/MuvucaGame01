using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementActivator : MonoBehaviour{

	[SerializeField]protected List<ActionableElement> actionableElements;

	protected virtual void ActivateAll(){
		foreach (ActionableElement a_element in actionableElements) {
			if(a_element!=null && a_element.isActiveAndEnabled){
				a_element.Activate();
			}
		}
	}

	protected virtual void DeactivateAll(){
		foreach (ActionableElement a_element in actionableElements) {
			if(a_element!=null && a_element.isActiveAndEnabled){
				a_element.Deactivate();
			}
		}
	}

	public List<ActionableElement> ActionableElements {
		get {
			return this.actionableElements;
		}
		set {
			actionableElements = value;
		}
	}

	public void AddActionableElement(ActionableElement act_elem){
		actionableElements.Add (act_elem);
	}

	public void RemoveActionableElement(ActionableElement act_elem){
		actionableElements.Remove (act_elem);
	}
}

