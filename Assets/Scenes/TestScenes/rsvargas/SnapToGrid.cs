using UnityEngine;
using System.Collections;

// This script is executed in the editor
[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour
{
#if UNITY_EDITOR
    public bool snapToGrid = true;
    public float snapValue = 0.5f;

    public bool sizeToGrid = false;
    public float sizeValue = 0.25f;

    // Adjust size and position
    void Update()
    {
        if (snapToGrid)
            transform.position = RoundTransform(transform.position, snapValue);

        if (sizeToGrid)
            transform.localScale = RoundTransform(transform.localScale, sizeValue);
    }

    // The snapping code
    private Vector3 RoundTransform(Vector3 v, float snapValue)
    {
        return new Vector3
        (
            snapValue * Mathf.Round(v.x / snapValue),
            snapValue * Mathf.Round(v.y / snapValue),
            v.z
        );
    }
#endif
}