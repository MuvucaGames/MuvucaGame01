using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Balloon : MonoBehaviour {

	private Sentence.Info sentenceInfo;
	public delegate void Callback();
	private event Callback callback;

	[SerializeField]
	private Image backgroundImg;
	[SerializeField]
	private Image imgTmp;

	void Update () 
	{
		if (sentenceInfo != null) {



		}
	}

	public void Init(Sentence.Info sentenceInfo, Callback callback){
		this.sentenceInfo = sentenceInfo;
		this.callback = callback;

		if(sentenceInfo.Hooker!=null)
			this.transform.position = sentenceInfo.Hooker.transform.position;

		SetCanvasSizeAndImages ();

		SetSpringJoint ();

		Destroy(gameObject, sentenceInfo.timeInSeconds);
	}

	private void SetCanvasSizeAndImages(){
		Canvas canvas = GetComponentInChildren<Canvas> ();
		int imagesAmount = sentenceInfo.sentence.images.Count;
		RectTransform canvasRectTransform = canvas.GetComponent<RectTransform> ();
		canvasRectTransform.sizeDelta = Vector2.Scale (canvasRectTransform.sizeDelta, new Vector2 (imagesAmount,1));


		for (int i = 0; i<imagesAmount; i++) {
			Image img = Instantiate(imgTmp);
			img.rectTransform.SetParent(backgroundImg.rectTransform);
			img.rectTransform.anchorMin = new Vector2(i*(1f/imagesAmount),0f);
			img.rectTransform.anchorMax = new Vector2((i+1)*(1f/imagesAmount),1f);
			img.rectTransform.offsetMin = Vector2.zero;
			img.rectTransform.offsetMax = Vector2.zero;
			img.sprite = sentenceInfo.sentence.images[i];
			img.gameObject.SetActive(true);
		}

	}

	private void SetSpringJoint(){
		SpringJoint2D springJoint2D = GetComponent<SpringJoint2D> ();
		if (sentenceInfo.Hooker.GetComponent<Rigidbody2D> ()!= null) {
			
			springJoint2D.connectedBody = sentenceInfo.Hooker.GetComponent<Rigidbody2D> ();
		} else {
			//springJoint2D.enabled = false;
		}
	}

	public void OnDestroy(){
		if (sentenceInfo != null) {
			callback ();	
		}
	}

	public enum TypeOfBalloon{
		Normal,
		Think,
		Shout
	}


}
