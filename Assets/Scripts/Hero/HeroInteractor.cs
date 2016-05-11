using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroInteractor : MonoBehaviour {

	private List<GameObject> ladders = new List<GameObject>();
	private List<GameObject> carriableObjects = new List<GameObject>();
	private List<GameObject> actionableObjects = new List<GameObject>();
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.GetComponent<ILadder>()!=null)
			ladders.Add (other.gameObject);
		if (other.gameObject.GetComponent<IHeroActionable>()!=null)
			actionableObjects.Add (other.gameObject);
		else if (other.gameObject.GetComponent<Carriable>()!=null)
			carriableObjects.Add (other.gameObject);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<ILadder>()!=null)
			ladders.Remove (other.gameObject);
		else if (other.gameObject.GetComponent<IHeroActionable>()!=null)
			actionableObjects.Remove (other.gameObject);
		else
			carriableObjects.Remove (other.gameObject);
	}

	public GameObject ladder { 
		get { 
			if (ladders.Count > 0)
				return ladders [0];
			return null;
			}
	}

	public GameObject actionableObject {
		get { 
			if (actionableObjects.Count > 0)
				return actionableObjects [0];
			return null;
		}
	}

	public GameObject carriableObject {
		get { 
			if (carriableObjects.Count > 0)
				return carriableObjects [0];
			return null;
		}
	}

}
