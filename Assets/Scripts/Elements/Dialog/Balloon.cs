using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {

	private Dialog.SentenceInfo sentenceInfo;
	public delegate void Callback();
	private event Callback callback;

	void Update () 
	{
		if (sentenceInfo != null) {



		}
	}

	public void Init(Dialog.SentenceInfo sentenceInfo, Callback callback){
		this.sentenceInfo = sentenceInfo;
		this.callback = callback;

		if(sentenceInfo.Hooker!=null)
			this.transform.position = sentenceInfo.Hooker.transform.position;

		Canvas canvas = GetComponentInChildren<Canvas> ();

		int imagesAmount = sentenceInfo.sentence.images.Count;
		RectTransform canvasRectTransform = canvas.GetComponent<RectTransform> ();
		canvasRectTransform.sizeDelta = Vector2.Scale (canvasRectTransform.sizeDelta, new Vector2 (imagesAmount,1));

		SpringJoint2D springJoint2D = GetComponent<SpringJoint2D> ();
		springJoint2D.connectedBody = sentenceInfo.Hooker.GetComponent<Rigidbody2D> ();

		Destroy(gameObject, sentenceInfo.timeInSeconds);
		enabled = true;
	}

	public void OnDestroy(){
		if (sentenceInfo != null) {
			callback ();	
		}
	}


}
