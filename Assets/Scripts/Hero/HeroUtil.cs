using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///   Hero Utilities class.
/// </summary>

/// This is utility class for heroes. Use wisely.
/// 
/// Heroes are treated in a different way beacause they are special, and without them well...
/// it is impossible to play the game. That´s the reason we must track them out and 
/// be careful to not lose control of them.

public static class HeroUtil
{
    /// <summary>
    ///   Discover wich controllable is active right now, if any.
    /// </summary>
    private static Hero lastActiveHero = null;
    private static Controllable controllableObject = null;

    /// <summary>
    ///   Discover wich controllable is active right now, if any.
    /// </summary>
    /// <returns> Active Hero </returns>
    private static Hero GetActiveHero()
    {        
        if (InputManager.Instance.heroStrong.IsActive)
        {
            return InputManager.Instance.heroStrong;
        }
        else if (InputManager.Instance.heroFast.IsActive)
        {
            return InputManager.Instance.heroFast;
        }
        return null;
    }

    /// <summary>
    ///   Disable controls and camera calling the procedure _OnFocusOut for all 
    ///   controllable objects(including heroes)
    /// </summary>
    public static void DisableControlAll()
    {
        InputManager.Instance.heroFast._OnFocusOut();
        InputManager.Instance.heroStrong._OnFocusOut();
        if (controllableObject != null) controllableObject._OnFocusOut();
    }

    /// <summary>
    ///   Change to next Hero.
    /// </summary>
	public static void ChangeHero()
    {
        HeroStrong heroStrong = InputManager.Instance.heroStrong;
		HeroFast heroFast = InputManager.Instance.heroFast;

        Hero hero = GetActiveHero();
        if (hero == null)
        {
            DisableControlAll();
            lastActiveHero._OnFocus();
        }
        else if (hero.Equals(heroStrong))
        {
            heroStrong._OnFocusOut();
            heroFast._OnFocus();
            lastActiveHero = heroFast;
        }
        else if (hero.Equals(heroFast))
        {
            heroFast._OnFocusOut();
            heroStrong._OnFocus();
            lastActiveHero = heroStrong;
        }
        else
        {
            DisableControlAll();
            heroStrong._OnFocus();
            lastActiveHero = heroStrong;
        }
        SoundManager.Instance.SendMessage("PlaySFXSwap");
    }

    /// <summary>
    ///   Change to Controllable Object(non hero).
    /// </summary>
    public static void ChangeToControllable()
    {
        Debug.AssertFormat(controllableObject != null, "Controllable Object not set");
        Debug.AssertFormat(GetActiveHero() != null, "No Hero being controlled");
        if (HeroUtil.controllableObject != null)
        {
            lastActiveHero = GetActiveHero();
            lastActiveHero._OnFocusOut();
            controllableObject._OnFocus();
            SoundManager.Instance.SendMessage("PlaySFXSwap");
        }
    }

    /// <summary>
    ///   Sets the Controllable Object.
    /// </summary>
    /// <param name = "obj">
    ///   Object to be set as the controllable that is activated
    ///   when action happens(non hero).
    /// </param>
    public static void SetControllableObject(Controllable obj)
    {
        if (obj.GetComponent<Hero>() == null)
        {
            controllableObject = obj;
        }
    }

    /// <summary>
    ///   Resets the Controllable Object.
    /// </summary>
    /// <param name = "obj">
    ///   Reset object to be controlled when action happens. 
    ///   While reset, no object will receive keyboard or camera focus.
    /// </param>
    public static void ResetControllableObject(Controllable obj)
    {
        if (obj.GetComponent<Hero>() != null)
        {
            controllableObject = null;
        }
    }
}