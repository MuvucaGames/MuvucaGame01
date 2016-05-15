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
		StartCoroutine(Move());
    }
    
    private IEnumerator Move()
    {
		while(Vector3.Distance(transform.position, pointB)!=0){
			float step = speed * Time.deltaTime;        
			transform.position = Vector3.MoveTowards(transform.position, pointB, step);
			yield return null;
		}

        print("Destination reached");
        yield return new WaitForSeconds(3f);
        print("Coroutine finished");
    }
}

