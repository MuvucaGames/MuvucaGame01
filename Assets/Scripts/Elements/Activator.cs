using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Activator : MonoBehaviour{

	[SerializeField]protected List<ActionableElement> actionableElementsList;

	protected virtual void ActivateAll(){
		foreach (ActionableElement actionableElement in actionableElementsList) {
			if(actionableElement!=null && actionableElement.isActiveAndEnabled){
				actionableElement.Activate();
			}
		}
	}

	protected virtual void DeactivateAll(){
		foreach (ActionableElement actionableElement in actionableElementsList) {
			if(actionableElement!=null && actionableElement.isActiveAndEnabled){
				actionableElement.Deactivate();
			}
		}
	}

	public List<ActionableElement> ActionableElements {
		get {
			return this.actionableElementsList;
		}
		set {
			actionableElementsList = value;
		}
	}

	public void AddActionableElement(ActionableElement actionableElement){
		actionableElementsList.Add (actionableElement);
	}

	public void RemoveActionableElement(ActionableElement actionableElement){
		actionableElementsList.Remove (actionableElement);
	}
}

