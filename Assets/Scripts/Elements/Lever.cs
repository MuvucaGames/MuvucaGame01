using UnityEngine;
using System.Collections;

public class Lever : ElementActivator, IHeroActionable
{
    [SerializeField]
    private bool isTimeBased = false;
    [SerializeField]
    private float leverDelay = 0f;
    private float timer;
    private bool isActive;

	[SerializeField]private SpriteRenderer deactiveRenderer = null;
	[SerializeField]private SpriteRenderer activeRenderer = null;


    public bool IsActive
    {
        get
        {
            return isActive;
        }
    }

	// Use this for initialization
	void Awake () {
        isActive = false;
	}

	void Update () {
        if (isTimeBased && isActive)
        {
            timer += Time.deltaTime;
            if (timer >= leverDelay)
                ChangeState();
        }
	}

    private void ChangeState()
    {
        timer = 0f;
        isActive = !isActive;
        if (isActive)
			ActivateAll ();
		else
			DeactivateAll ();

        Swap();
    }

    private void Swap()
    {
		activeRenderer.gameObject.SetActive (isActive);
		deactiveRenderer.gameObject.SetActive (!isActive);

    }

	public void OnHeroActivate ()
	{
		ChangeState ();
	}
}
