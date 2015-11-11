using UnityEngine;
using System.Collections;

/*
 * WarpZoneDoor.cs
 */

public class WarpZoneDoor : Door
{
    public Hero hero;
    public Door doorTarget;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 doorTargetPosition = doorTarget.transform.position;
        Vector3 heroPosition = new Vector3(hero.transform.position.x, hero.transform.position.y,
            hero.transform.position.z);
        if (this.isDoorOpened && doorTarget.isDoorOpened)
        {
            // TODO: Character goes at lightspeed to linked target door. Fix
            // this using realistic travel time to another target door at some realistic velocity
            hero.transform.Translate(doorTargetPosition - heroPosition, Space.World);
        }
    }

    public void OnExitTrigger2D(Collider2D other)
    {
        
    }

    public override void Activate()
    {
        
    }

    public override void Deactivate()
    {
    }
}
