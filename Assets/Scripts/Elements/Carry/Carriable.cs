using UnityEngine;
using System.Collections;

public abstract class Carriable : MonoBehaviour {
	public bool isBeingCarried=false;
	//this commented code is for, just in te case, the hero transfer this element to the other hero
	//public Hero hero=null;
	public abstract bool isHeavy();
}

