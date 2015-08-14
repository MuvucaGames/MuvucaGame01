using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CameraParamTools : MonoBehaviour {

	public Camera cam;
	public Slider camSizeSlider;
	public Text camSizeText;

	public void Start(){
		camSizeSlider.value = cam.orthographicSize;
	}

	public void ChangeCameraDistsance(float f){
		cam.orthographicSize = f;
		camSizeText.text = f.ToString("F1");
	}
}
