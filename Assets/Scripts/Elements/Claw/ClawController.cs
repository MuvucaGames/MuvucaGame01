using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///   Controls claws action and movement.
/// </summary>
///  Controls claws action and movement. Talks to other controllers such as 
///  HeroController and CameraController, resulting in changing focus from Hero
///  to the Claw.
///  
///  To activate, one of the heroes must stand within the Terminal bounds, and press the 
///  button corresponding to Change Hero and then control and camera are transferred to
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
///  - When hero collides with cliff above claw hanging object, 
///    he moves outside the platform
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
    private HeroStrong heroStrong = null;
    private HeroFast heroFast = null;
    private Stack<SpriteRenderer> nodes = new Stack<SpriteRenderer>();
    private Stack<SpriteRenderer> nodesPool = new Stack<SpriteRenderer>();
    private InvisibleAreaTrigger Terminal = null;
    private CameraController cameraControl = null;
    private Hero activeHero = null;

    private ClawMechanism clawMechanism;
    private ClawNode clawNode;
    private ClawPerSe clawPerSe;
    
    void Awake() {
        clawMechanism = GetComponentInChildren<ClawMechanism>();
        clawNode = GetComponentInChildren<ClawNode>();
        clawPerSe = GetComponentInChildren<ClawPerSe>();
        heroStrong = FindObjectOfType<HeroStrong> ();
        heroFast = FindObjectOfType<HeroFast>();
        Terminal = GetComponentInChildren<InvisibleAreaTrigger>();
        clawLastPosition = clawNode.GetComponent<Renderer>().bounds.center;
        cameraControl = FindObjectOfType<CameraController>();
    }

    /// <summary>
    ///    Momevent code for claw parts.
    /// </summary>
    /// <param name="direction">
    ///   Direction vector applied to claw
    /// </param>
    private void MoveClaw(Vector3 direction)
    {
        if ((clawNode.itemsOverPlatform.Count > 0) && clawPerSe.closedClaw) {
            foreach (Collider2D coll__ in clawNode.itemsOverPlatform)
                coll__.attachedRigidbody.transform.Translate(direction);
        }
        if (clawCenter.y <= clawNode.clawInitialPos.y &&
            clawCenter.y >= clawNode.clawInitialPos.y - minHeight)
        {
            clawPerSe.transform.Translate (direction);
        }   
        else
        {
            if ((verticalPos <= 0 && clawCenter.y > clawNode.clawInitialPos.y) ||
                (verticalPos >= 0 && clawCenter.y < clawNode.clawInitialPos.y - minHeight))
            {
                direction.y = 0;
            }
            clawPerSe.transform.Translate (direction);
        }
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
                float positionY;
                if (nodes.Count > 0)
                {
                    positionY = nodes.Peek().GetComponent<SpriteRenderer>().bounds.max.y;
                }
                else
                {
                    positionY = clawMechanism.clawInitialPos.y;
                }
                clone = nodesPool.Pop();
                clone.GetComponent<SpriteRenderer> ().enabled = true;
                clone.transform.position = new Vector3(clawCenter.x, positionY, clawCenter.z);
            }
            clone.transform.SetParent (clawPerSe.transform);
            nodes.Push (clone);
        }
    }

    // This could be global method
    /// <summary>
    ///   Discover wich hero is active
    /// </summary>
    /// <returns> Active hero </returns>
    private Hero GetActiveHero()
    {        
        if (heroStrong.m_isActive)
        {
            return heroStrong;
        }
        else if (heroFast.m_isActive)
        {
            return heroFast;
        }
        return null;
    }

    void Update() {
        if (Terminal.GetQttHeroesInside() > 0) {
            heroFast.changeHeroAllowed = false;
            heroStrong.changeHeroAllowed = false;
            if (active) {
                if (Input.GetKeyDown (KeyCode.E)) {
                    action = !action;
                }

                if (Input.GetButtonDown("ChangeHero")) 
                {
                    active = false;
                    heroStrong.GetComponent<HeroControl>().enabled = true;
                    heroFast.GetComponent<HeroControl>().enabled = true;
                    if (activeHero) activeHero.m_isActive = true;
                    else
                    {
                        heroStrong.m_isActive = true;
                    }
                    if (cameraControl.interactiveFocusableObject)
                        cameraControl.interactiveFocusableObject = null;
                }
            }
            else
            {   
                if (Input.GetButtonDown("ChangeHero")) 
                {
                    activeHero = GetActiveHero();
                    heroFast.m_isActive = false;
                    heroStrong.m_isActive = false;
                    heroFast.GetComponent<HeroControl>().enabled = false;
                    heroStrong.GetComponent<HeroControl>().enabled = false;
                    cameraControl.interactiveFocusableObject = clawPerSe.gameObject;
                    active = true;
                    activeHero.StopWalk();
                }
            }
        }
        else
        {
            heroFast.changeHeroAllowed = true;
            heroStrong.changeHeroAllowed = true;
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
            horizontalPos = verticalPos = 0f;
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
        if (clawPerSe.clawInitialPos == Vector3.zero)
        {
            clawPerSe.clawInitialPos = clawPerSe.transform.position;
        }
        SpriteRenderer sR = clawPerSe.GetComponentInChildren<SpriteRenderer>();
        float clawSizeY = sR.bounds.extents.y;
        Vector3 From = new Vector3(clawPerSe.transform.position.x - 1f,
                                   clawPerSe.clawInitialPos.y + clawSizeY,
                                   clawPerSe.transform.position.z);
        Vector3 To = new Vector3(clawPerSe.transform.position.x + 1f,
                                 clawPerSe.clawInitialPos.y - minHeight - clawSizeY,
                                 clawPerSe.transform.position.z);
        ClawUtils.allowDrawing = allowDrawing;
        ClawUtils.DrawRectangle(From, To, Color.red);
    }
}
