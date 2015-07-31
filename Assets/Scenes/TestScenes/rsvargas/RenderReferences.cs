using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RenderReferences : MonoBehaviour {

	public Vector2 distance = new Vector2(-5, 0);
	public GameObject labelPrefab;

	private List<GameObject> labels;
	void Render()
	{
		foreach (Transform child in transform) {
			Vector3 desired = new Vector3(child.position.x + distance.x, child.position.y + distance.y);
			string str = string.Format("{0}m", child.position.y );
			GameObject label = GetObject( child.name+str );
			label.transform.position = desired;
			label.name = child.name + str;
			label.GetComponentsInChildren<Text>()[0].text = str;
			// DO NOT set parent here, it will loop infinitely
			//label.transform.SetParent(transform);
		}
	}

	private GameObject GetObject(string str) {
		foreach (GameObject l in labels) {
			if (l.name == str) {
				return l;
			}
		}
		GameObject obj = Instantiate(labelPrefab) as GameObject;
		obj.name = str;
		labels.Add(obj);
		return obj;
	}


	// Use this for initialization
	void Start () {
		labels = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		Render ();
	}
}
