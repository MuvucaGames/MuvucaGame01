using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InvisibleAreaTrigger : Activator {

	public bool triggerOnlyOnce = false;
    public bool allowDeactivate = false;
    public bool triggersWithBothInside = false;
    private bool alreadyTrigged = false;
    private HashSet<Hero> heroesInside = new HashSet<Hero>();

	void OnTriggerEnter2D(Collider2D other) {
		if (triggerOnlyOnce && alreadyTrigged) 
		{
			return;
		}

		Hero hero = other.gameObject.GetComponentInParent<Hero> ();

		if (hero!=null)
        {
			heroesInside.Add(hero);
        }

		if (heroesInside.Count == 2 && triggersWithBothInside) {
            ActivateAll();
            alreadyTrigged = true;
		} else if (heroesInside.Count > 0 && !triggersWithBothInside)
        {
            ActivateAll();
            alreadyTrigged = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
		Hero hero = other.gameObject.GetComponentInParent<Hero> ();
		if (hero!=null)
        {
			heroesInside.Remove(hero);
        }

		if ((heroesInside.Count == 0) && alreadyTrigged && allowDeactivate) 
        {
            DeactivateAll();
        }
    }

    public int GetQttHeroesInside()
    {
        return heroesInside.Count;
    }
}