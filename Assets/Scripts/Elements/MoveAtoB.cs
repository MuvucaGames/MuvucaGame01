using UnityEngine;
using UnityEditor;
using System.Collections;

public class MoveAtoB : MonoBehaviour
{
    [SerializeField]
    private Vector3 pointA;
    [SerializeField]
    private Vector3 pointB = new Vector3(2, 0, 0);
    [Range(0.0f, 20.0f)]
    public float speed = 2f;
    void Awake()
    {
        pointA = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        pointB = transform.TransformPoint(pointB);
    }
    void FixedUpdate()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {      
        float step = speed * Time.deltaTime;        
        transform.position = Vector3.MoveTowards(transform.position, pointB, step);
        yield return null;
    }
}

