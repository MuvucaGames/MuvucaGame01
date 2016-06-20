using UnityEngine;
using System.Collections;

/// <summary>
/// Activator Terminal for Controllable Objects.
/// </summary>
public class Terminal : InvisibleAreaTrigger, IHeroActionable
{
    public void OnHeroActivate()
    {
        HeroUtil.ChangeToControllable();
    }
}
 