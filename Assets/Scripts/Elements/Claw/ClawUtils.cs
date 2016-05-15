using UnityEngine;
using System.Collections;

/// <summary>
///   Drawing utils.
/// </summary>
/// ### TODO:
/// - Extend utils to Elements.
/// - Maybe rename to something like Utils?? DrawUtils??
///
/// \sa ClawController
public static class ClawUtils
{   
    /// <summary>
    ///   State of drawing.
    /// </summary> 
    public static bool allowDrawing = true;
    /// <summary>
    ///   Draw Rectangle using Gizmos with specified colors
    /// </summary>
    /// <param name="From">
    ///   Upper left point of rectangle
    /// </param>
    /// <param name="From">
    ///   Lower right point of rectangle
    /// </param>
    /// <param name="Color">
    ///   Line color.
    /// </param>
    public static void DrawRectangle(Vector3 From, Vector3 To, Color color)
    {
        if (allowDrawing)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(From, new Vector3(To.x, From.y, To.z));
            Gizmos.DrawLine(From, new Vector3(From.x, To.y, To.z));
            Gizmos.DrawLine(new Vector3(To.x, From.y, From.z),To);
            Gizmos.DrawLine(new Vector3(From.x, To.y, From.z),To);            
        }
    }
}