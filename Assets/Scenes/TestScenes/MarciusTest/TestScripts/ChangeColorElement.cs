using UnityEngine;
using System.Collections;

public class ChangeColorElement : MonoBehaviour, IActionableElement {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Activate()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void Deactivate()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
