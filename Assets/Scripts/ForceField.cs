using UnityEngine;
using System.Collections;

public class ForceField : MonoBehaviour, IActionableElement
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("TouchedForceField");
        }
    }

    public void Activate()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Deactivate()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}