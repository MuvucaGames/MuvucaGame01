using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///   Controls claws action and movement.
/// </summary>
///  To activate, the hero must stand within the Terminal bounds and press the 
///  corresponding button to 'Change Hero', and then control and camera are transferred to
///  the claw.
///
///  To movement claw, player must use arrows keys, and when the claw is in direct contact
///  with and object of type 'CarriableLight', user must press button Action in order
///  to lift the object. Pressing action with object lifted will cause the object to fall
///  and claw to open.
///
///  ### TODO: 
///  - Extend concept for Object Pool to the role build 
///    [Object Pool Pattern](https://en.wikipedia.org/wiki/Object_pool_pattern).
///
///  ### KNOWN BUGS:
///  - Sometimes, nodes are placed with some distance between them.
///    This depends on some physics matters.
///
///  \sa Claw, ClawNode, ClawPerSe, ClawMechanism, InvisibleAreaTrigger, 
///  CameraController, CarriableLight, HeroControl

[ExecuteInEditMode]
public class ClawController : MonoBehaviour {
    
    [SerializeField][Range(0f,10f)]private float horizontalVelocity = 1.5f;
    [SerializeField][Range(0f,10f)]private float verticalVelocity = 1.2f;
    [SerializeField][Range(0f,20f)]private float minHeight = 4f;
    [SerializeField]private bool allowDrawing = true;
                                           
    private Vector3 clawLastPosition;
    private Vector3 clawCenter;
    private float horizontalPos;
    private float verticalPos;
    [HideInInspector]public bool active = false;
    [HideInInspector]public bool action = false;
    private Stack<SpriteRenderer> nodes = new Stack<SpriteRenderer>();
    private Stack<SpriteRenderer> nodesPool = new Stack<SpriteRenderer>();

    private ClawMechanism clawMechanism;
    private ClawNode clawNode;
    private ClawPerSe clawPerSe;
    
    void Awake() {
        clawMechanism = GetComponentInChildren<ClawMechanism>();
        clawNode = GetComponentInChildren<ClawNode>();
        clawPerSe = GetComponentInChildren<ClawPerSe>();
        clawLastPosition = clawNode.GetComponent<Renderer>().bounds.center;
    }

    /// <summary>
    ///    Momevent code for claw parts.
    /// </summary>
    /// <param name="direction">
    ///   Direction vector applied to claw
    /// </param>
    public void MoveClaw(Vector3 direction)
    {
        if ((verticalPos <= 0 && clawCenter.y > clawNode.clawInitialPos.y) ||
            (verticalPos >= 0 && clawCenter.y < clawNode.clawInitialPos.y - minHeight))
        {
            direction.y = 0;
        }
        clawPerSe.transform.Translate (direction);
    }

    /// <summary>
    ///   Main axis of the claw is duplicated or deleted whether the claw is going
    ///   downwards or upwards
    /// </summary>
    private void NodeUpdate()
    {
        if (nodes.Count > 0) {
            SpriteRenderer clone = null;
            clone = nodes.Peek ();
            clawLastPosition = clone.GetComponent<Collider2D> ().bounds.max;
        } else {
            clawLastPosition = clawNode.GetComponent<Collider2D>().bounds.max;
        }
        if (clawMechanism.GetComponent<Renderer> ().bounds.max.y - 0.04f < clawLastPosition.y && nodes.Count > 0) {
            SpriteRenderer clone = null;
            clone = nodes.Pop ();
            clone.GetComponent<SpriteRenderer> ().enabled = false;
            nodesPool.Push(clone);
        } 
        else if (clawMechanism.GetComponent<Renderer> ().bounds.min.y + 0.07f > clawLastPosition.y) {
            SpriteRenderer clone = null;
            if (nodesPool.Count == 0)
            {
                clone = (SpriteRenderer)Instantiate (clawNode.GetComponent<SpriteRenderer> (),
                                                     new Vector3(clawCenter.x, clawMechanism.clawInitialPos.y, clawCenter.z),
                                                     clawNode.transform.rotation);
            }
            else 
            {
                // Create chain of nodes
                clone = nodesPool.Pop();
                clone.GetComponent<SpriteRenderer> ().enabled = true;
                clone.transform.position = new Vector3(clawCenter.x, clawMechanism.clawInitialPos.y, clawCenter.z);
            }
            clone.transform.SetParent (clawPerSe.transform);
            nodes.Push (clone);
        }
    }

    void Update() {
        if (clawPerSe.IsActive) {
            active = true;
            if (Input.GetKeyDown (KeyCode.E)) {
                action = !action;
            }
        }
        else
        {
            active = false;
        }
    }

    void FixedUpdate()
    {
        clawCenter = clawNode.GetComponent<Renderer>().bounds.center;
        float horizontalAxis = Input.GetAxis ("Horizontal");
        float verticalAxis = Input.GetAxis ("Vertical");
        
        if (horizontalAxis != 0 || verticalAxis != 0 || !active)
        {
            horizontalPos = horizontalAxis * horizontalVelocity;
            verticalPos = verticalAxis * verticalVelocity;
        } 
        else
        {
            horizontalPos = verticalPos = 0f;            
        }

        Vector3 movement = new Vector3 (horizontalPos, -verticalPos, 0);
        clawCenter = clawNode.GetComponent<Renderer>().bounds.center;
        NodeUpdate();
        // Everything is fine. No limits hit.
        if (active && !clawMechanism.isOnLeftLimit && !clawMechanism.isOnRightLimit)
        {
            if (!clawPerSe.interactiveObject)
            {
                action = false;
                clawPerSe.GetComponent<BoxCollider2D> ().enabled = true;
            }
            MoveClaw(movement * Time.deltaTime);   
        }
        // In case claw hit one of the limits...
        else
        {
            if (clawMechanism.isOnLeftLimit && horizontalPos >= 0 ||
                clawMechanism.isOnRightLimit && horizontalPos <= 0)
            {
                MoveClaw(movement * Time.deltaTime);
            }
                
            else if (clawMechanism.isOnLeftLimit && horizontalPos < 0 ||
                     clawMechanism.isOnRightLimit && horizontalPos > 0)
            {
                // Bounce effect
                if (movement.x != 0)
                {
                    movement.x = -1 * (movement.x);
                    MoveClaw(movement * Time.deltaTime * 0.25f);
                }
                else
                {
                    MoveClaw(verticalPos * Vector3.down * Time.deltaTime);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        // if claw per se has not been initialized yet, must set to the actiual position
        SpriteRenderer sR = clawPerSe.GetComponentInChildren<SpriteRenderer>();
        float clawSizeY = sR.bounds.extents.y;
        Vector3 From = new Vector3(clawPerSe.transform.position.x - 1f,
                                   clawPerSe.transform.position.y + clawSizeY,
                                   clawPerSe.transform.position.z);
        Vector3 To = new Vector3(clawPerSe.transform.position.x + 1f,
                                 clawPerSe.transform.position.y - minHeight - clawSizeY,
                                 clawPerSe.transform.position.z);
        ClawUtils.allowDrawing = allowDrawing;
        ClawUtils.DrawRectangle(From, To, Color.red);
    }
}
