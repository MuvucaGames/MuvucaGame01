using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Hero interactor - Gather all interactives Gameobjects that are triggered by the InteractorHero
/// </summary>
public class HeroInteractor : MonoBehaviour {
	/// <summary>
	/// The ladders - List of the ladders inside the Trigger
	/// </summary>
	private List<GameObject> ladders = new List<GameObject>();
	/// <summary>
	/// The carriable objects - List of Carriables inside the Trigger
	/// </summary>
	private List<GameObject> carriableObjects = new List<GameObject>();
	/// <summary>
	/// The actionable objects. list of the Actionables inside the Trigger
	/// </summary>
	private List<GameObject> actionableObjects = new List<GameObject>();
	/// <summary>
	/// Raises the trigger enter2 d event. Separate the interactives GameObjects in the categories
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.GetComponent<ILadder>()!=null)
			ladders.Add (other.gameObject);
		if (other.gameObject.GetComponent<IHeroActionable>()!=null)
			actionableObjects.Add (other.gameObject);
		else if (other.gameObject.GetComponent<Carriable>()!=null)
			carriableObjects.Add (other.gameObject);
	}
	/// <summary>
	/// Raises the trigger exit2 d event. Remove the interactives GameObjects
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<ILadder>()!=null)
			ladders.Remove (other.gameObject);
		if (other.gameObject.GetComponent<IHeroActionable>()!=null)
			actionableObjects.Remove (other.gameObject);
		else
			carriableObjects.Remove (other.gameObject);
	}
	/// <summary>
	/// Gets the top most ladder.
	/// </summary>
	/// <value>The ladder.</value>
	public GameObject ladder { 
		get { 
			if (ladders.Count > 0)
				return ladders [0];
			return null;
			}
	}
	/// <summary>
	/// Gets the top most actionable object.
	/// </summary>
	/// <value>The actionable object.</value>
	public GameObject actionableObject {
		get { 
			if (actionableObjects.Count > 0)
				return actionableObjects [0];
			return null;
		}
	}
	/// <summary>
	/// Gets the top most carriable object.
	/// </summary>
	/// <value>The carriable object.</value>
	public GameObject carriableObject {
		get { 
			if (carriableObjects.Count > 0)
				return carriableObjects [0];
			return null;
		}
	}

}
