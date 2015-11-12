using UnityEngine;
using System.Collections;

public class ActionableElementBase : ActionableElement
{
    private bool _isActivated;

    public override void Activate()
    {
        _isActivated = true;
    }

    public override void Deactivate()
    {
        _isActivated = false;
    }
    public bool isActivated
    {
        get { return _isActivated; }
    }
}
