using UnityEngine;
using System.Collections;

public class MainEspecialCamera : MonoBehaviour {

    Camera cam;

    public Transform trans;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 aux = new Vector3(cam.transform.position.x, cam.transform.position.y, -1);
        cam.transform.position = Vector3.Lerp(aux, trans.position, Time.deltaTime * 2);
	}
}
