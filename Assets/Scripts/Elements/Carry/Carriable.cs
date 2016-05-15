using UnityEngine;
using System.Collections;

public abstract class Carriable : MonoBehaviour {
	public bool isBeingCarried=false;
	public abstract bool isHeavy();
}

