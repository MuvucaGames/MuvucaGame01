using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
///   Follows GameObject
/// </summary>
public class Follower : MonoBehaviour 
{
    public Rigidbody2D body = null;
    public GameObject objectToFollow;
    public float radius = 10f;
    public float velocity = 2f;
    public int FrameToMakePath = 10;
    public float bounceAmplitude = 0.5f;
    public float minDistance = 0.1f;
    public bool hasBounceEffect = true;
    public bool isBouncing = false;
    public float t;

    private int Timer;
    private bool pathIsPossible = false;
    private Vector3 lastPathDirection;
    private Vector3 distance;

    void Start()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();   
        }
        Timer = FrameToMakePath;

        Debug.AssertFormat(objectToFollow != null, "There is no object to follow specified");
    }

    public void FixedUpdate()
    {
        Timer -= 1;
        if ((Timer <= 0f))
        {
            if (objectToFollow != null)
            {
                pathIsPossible = MakePath();
                Timer = FrameToMakePath;                
            }
        }

        if (pathIsPossible && !isBouncing)
        {
            if (distance.magnitude > minDistance)
            {
                transform.Translate(lastPathDirection*velocity*distance.magnitude*Time.deltaTime);
            }
            else
            {               
                if(hasBounceEffect)
                {
                    isBouncing = true;
         
                }
                else
                {
                    body.velocity = Vector3.zero;
                }
            }
        }

        if (isBouncing)
        {
            // Air bounce effect
            body.gameObject.transform.Translate(new Vector2(0, bounceAmplitude*Mathf.Cos(t)*Time.deltaTime));
            t += Mathf.PI*Time.deltaTime;
            if (t >= (2f*Mathf.PI))
            {
                t = 0;
            }

            if (distance.magnitude > bounceAmplitude)
            {
                isBouncing = false;
            }
        }
    }

    public bool MakePath()
    {
        Vector3 initialPosition = transform.position;
        Vector3 finalPosition;
    
        // In case the object does not have any Renderer attached
        if (objectToFollow.GetComponent<Renderer>() == null && 
            objectToFollow.GetComponentInChildren<Renderer>() == null)
        {
            finalPosition = objectToFollow.transform.position;
        }
        else
        {
            finalPosition = objectToFollow.GetComponentInChildren<Renderer>().bounds.max;
        }

        lastPathDirection = Vector3.Normalize(finalPosition - initialPosition);
        distance = finalPosition - initialPosition;

        if (distance.magnitude < radius)
        {
            return true;
        }
        return false;
    }
}