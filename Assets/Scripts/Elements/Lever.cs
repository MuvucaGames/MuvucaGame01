using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private ActionableElement actionableObject;
    [SerializeField]
    private bool isTimeBased;
    [SerializeField]
    private float leverDelay;
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
            actionableObject.SendMessage("Activate");
        else
            actionableObject.SendMessage("Deactivate");

        Swap();
    }

    private void Swap()
    {
		activeRenderer.gameObject.SetActive (isActive);
		deactiveRenderer.gameObject.SetActive (!isActive);

    }
}
