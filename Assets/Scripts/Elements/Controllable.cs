using UnityEngine;
using System.Collections;

/// <summary>
///   Class for objects that receive focus of keyboard and camera
/// </summary>
public class Controllable : ActionableElement
{
	private bool isActive = false;

	public bool IsActive {
		get { return isActive; }
		set { isActive = value; }
	}

    public override void Activate()
    {
        HeroUtil.SetControllableObject(this);
    }

    public override void Deactivate() 
    {
        HeroUtil.ResetControllableObject(this);
    }

    public void _OnFocus()
    {
        IsActive = true;
        CameraController.Instance.SetCameraControl(this);
        OnFocus();
    }

    public void _OnFocusOut()
    {
        IsActive = false;
        OnFocusOut();
    }

    /// <summary>
    ///   At Receiving focus of keyboard and camera, virtual method OnFocus is called.
    /// </summary>
    public virtual void OnFocus() {}

    /// <summary>
    ///   At leaving focus of keyboard and camera, virtual method OnFocusOut is called.
    /// </summary>
    public virtual void OnFocusOut() {}
}