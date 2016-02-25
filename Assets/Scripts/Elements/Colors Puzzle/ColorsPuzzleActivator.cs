using UnityEngine;
using System.Collections;

public class ColorsPuzzleActivator : Activator {

    private bool isActive = false;

    void Awake()
    {
        isActive = false;
    }

    void Update()
    {
        if (isActive)
        {
            ChangeState();
        }
    }

    private void ChangeState()
    {
        if (isActive)
            ActivateAll();
        else
            DeactivateAll();
        isActive = !isActive;
    }

    public void activate()
    {
        isActive = true;
    }
}
