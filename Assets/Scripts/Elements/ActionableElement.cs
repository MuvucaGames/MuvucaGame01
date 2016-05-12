using UnityEngine;
using System.Collections;
/// <summary>
/// Actionable element.
/// Extend this class to create a element that can be activated or deactivated by the Activator.
/// </summary>
public abstract class ActionableElement : MonoBehaviour {

	/// <summary>
	/// Activate this element
	/// </summary>
	public abstract void Activate();
	/// <summary>
	/// Deactivate this element.
	/// </summary>
	public abstract void Deactivate();
	
}
