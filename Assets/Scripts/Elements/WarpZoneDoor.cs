using UnityEngine;
using System.Collections;

/*
 * WarpZoneDoor.cs
 */

public class WarpZoneDoor : ActionableElement
{
    public bool isUnlocked;
    public bool isTimeBased;
    public bool isToggle;
    public bool bothHeroesNeeded;
    public HeroStrong heroStrong;
    public HeroFast heroFast;
    public WarpZoneDoor doorTarget;
    public float timeToTeleport = 0.5f;
    public float timeToChangeLockState = 1.0f;

    private bool justTeleported = false;
    private bool runningTimeBasedEffect;
    private bool isHeroStrongAtDoor = false;
    private bool isHeroFastAtDoor = false;
    private Rigidbody2D rigidBodyHeroStrong;
    private Rigidbody2D rigidBodyHeroFast;

    public void Start()
    {
        rigidBodyHeroStrong = heroStrong.GetComponent<Rigidbody2D>();
        rigidBodyHeroFast = heroFast.GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        // Figure out what hero activated door trigger
        Hero hero = whoTriggered(collider);
        // -Both source and target doors need to be unlocked in order to heroes warp to target!
        // -If player has just teleported to the door, do not teleport back, 
        // otherwise we will get infinite loop in case both doors are linked with one another.
        if (this.isUnlocked && doorTarget.isUnlocked && !justTeleported && (hero != null))
        {
            StartCoroutine(teleport(hero, collider));
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        this.justTeleported = false;
        if (collider.attachedRigidbody == rigidBodyHeroStrong)
        {
            isHeroStrongAtDoor = false;
        }
        else if (collider.attachedRigidbody == rigidBodyHeroFast)
        {
            isHeroFastAtDoor = false;
        }
    }

    IEnumerator teleport(Hero hero, Collider2D collider)
    {
        // TODO: when hero lands on the target door, he is not perfect grounded with the door
        // fine-tune this!
        float heroHeight = collider.bounds.extents.y;
        float doorTargetHeight = doorTarget.GetComponent<BoxCollider2D>().bounds.extents.y;
        Vector2 heroVerticalPositionOffset = new Vector2(0, doorTargetHeight - heroHeight);

        Vector2 doorTargetPosition = doorTarget.transform.position;

        if ((bothHeroesNeeded && isHeroStrongAtDoor && isHeroFastAtDoor))
        {
            heroStrong.gameObject.SetActive(false);
            heroFast.gameObject.SetActive(false);
            heroStrong.transform.position = (doorTargetPosition - heroVerticalPositionOffset);
            heroFast.transform.position = (doorTargetPosition - heroVerticalPositionOffset);
            doorTarget.justTeleported = true;
            yield return new WaitForSeconds(timeToTeleport);
            heroStrong.gameObject.SetActive(true);
            heroFast.gameObject.SetActive(true);
        }

        if(!bothHeroesNeeded)
        {
            hero.gameObject.SetActive(false);
            hero.transform.position = (doorTargetPosition - heroVerticalPositionOffset);
            doorTarget.justTeleported = true;
            yield return new WaitForSeconds(timeToTeleport);
            hero.gameObject.SetActive(true);
        }
    }

    public Hero whoTriggered(Collider2D collider)
    {
        if (collider.attachedRigidbody == rigidBodyHeroStrong)
        {
            isHeroStrongAtDoor = true;
            return heroStrong;
        }
        else if (collider.attachedRigidbody == rigidBodyHeroFast)
        {
            isHeroFastAtDoor = true;
            return heroFast;
        }
        return null;
    }

    public override void Activate()
    {
        if (isTimeBased)
        {
            if (!runningTimeBasedEffect)
            {
                runningTimeBasedEffect = true;
                isUnlocked = !isUnlocked;
            }
        }
        else if (isToggle)
        {
            isUnlocked = !isUnlocked;
        }
        else
        {
            isUnlocked = true;
        }
    }

    public override void Deactivate()
    {
        if (isTimeBased)
        {
            StartCoroutine(timeRemainingToChangeActiveState(timeToChangeLockState));
        }
    }

    IEnumerator timeRemainingToChangeActiveState(float time)
    {
        yield return new WaitForSeconds(time);
        isUnlocked = !isUnlocked;
        runningTimeBasedEffect = false;
    }
}
