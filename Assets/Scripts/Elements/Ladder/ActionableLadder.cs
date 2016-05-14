using UnityEngine;
using System.Collections;

public class ActionableLadder : ActionableElement, ILadder
{

    private bool _isActivated;
    public float initialHeight;
    public float finalHeight;

    void Start()
    {
        Vector3 scale = transform.localScale;
        scale.y = initialHeight;
        transform.localScale = scale;

    }
    public override void Activate()
    {
        Vector3 scale = transform.localScale;
        scale.y = finalHeight;
        transform.localScale = scale;
        Debug.Log("ativou");
        _isActivated = true;       
    }

    public override void Deactivate()
    {
        Vector3 scale = transform.localScale;
        scale.y = initialHeight;
        transform.localScale = scale;
        Debug.Log("desativou");
        _isActivated = false;
    }
    public bool isActivated
    {
        get { return _isActivated; }
    }



}