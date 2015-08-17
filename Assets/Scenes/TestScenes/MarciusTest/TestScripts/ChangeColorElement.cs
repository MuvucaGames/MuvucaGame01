using UnityEngine;
using System.Collections;

public class ChangeColorElement : ActionableElement {

    
	public override void Activate ()
	{
		gameObject.GetComponent<SpriteRenderer>().color = Color.green;
	}

	public override void Deactivate ()
	{
		gameObject.GetComponent<SpriteRenderer>().color = Color.red;
	}
    
}
