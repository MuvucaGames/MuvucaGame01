using UnityEngine;
using System.Collections;

/*
 * WarpZoneDoor.cs
 */

public class WarpZoneDoor : ActionableElement
{
    public Hero hero;
    public WarpZoneDoor doorTarget;
    public bool isDoorOpened;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 doorTargetPosition = doorTarget.transform.position;
        Vector3 heroPosition = hero.transform.position;
        if (this.isDoorOpened && doorTarget.isDoorOpened)
        {
            // TODO: Character goes at lightspeed from his position to linked target door. Fix
            // this using realistic travel time to another target door at some realistic velocity
            hero.transform.Translate(doorTargetPosition - heroPosition, Space.World);
        }
    }

    public void OnExitTrigger2D(Collider2D other)
    {
    }

    public override void Activate()
    {
        isDoorOpened = true;
    }

    public override void Deactivate()
    {
    }
}
