using UnityEngine;
using System.Collections;

public class ForceField : ActionableElement
{
    public override void Activate()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public override void Deactivate()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
