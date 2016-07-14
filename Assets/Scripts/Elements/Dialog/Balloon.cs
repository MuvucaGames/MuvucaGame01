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
	[SerializeField]
	private Sprite spriteBalloonNormal;
	[SerializeField]
	private Sprite spriteBalloonThink;
	[SerializeField]
	private Sprite spriteBalloonShout;
	[SerializeField]
	private Vector2 margin=new Vector2(0.2f, 0.2f);
	[SerializeField]
	private float extraMarginWidthToOneIcon=0.3f;
	[SerializeField]
	private Vector3 vertex1Triangle = new Vector3 (-0.3f, 0, 0);
	[SerializeField]
	private Vector3 vertex2Triangle = new Vector3 (0.2f, 0, 0);
	private Renderer hookerRenderer=null;
	void Update () 
	{
		if (sentenceInfo != null) {
			Mesh mesh = GetComponent<MeshFilter>().mesh;
			mesh.Clear();

			if (hookerRenderer != null) {
				Vector3 hookerVertex = transform.InverseTransformPoint(new Vector3 (hookerRenderer.bounds.center.x, hookerRenderer.bounds.max.y, 0));
				mesh.vertices = new Vector3[] { vertex1Triangle, vertex2Triangle, hookerVertex};
				mesh.uv = new Vector2[] { new Vector2 (0, 0), new Vector2 (0, 1), new Vector2 (1, 1) };
				mesh.triangles = new int[] { 0, 1, 2 };
			}

		}
	}

	public void Init(Sentence.Info sentenceInfo, Callback callback){
		this.sentenceInfo = sentenceInfo;
		this.callback = callback;

		if (sentenceInfo.Hooker != null) {
			hookerRenderer = sentenceInfo.Hooker.GetComponent<Renderer> ();
			if (hookerRenderer == null)				
				hookerRenderer = sentenceInfo.Hooker.GetComponentInChildren<Renderer> ();
			
			if (hookerRenderer != null)
				this.transform.position = new Vector3 (sentenceInfo.Hooker.transform.position.x, sentenceInfo.Hooker.transform.position.y + hookerRenderer.bounds.size.y);
			else
				this.transform.position = new Vector3 (sentenceInfo.Hooker.transform.position.x, sentenceInfo.Hooker.transform.position.y);
		}

		SetCanvasSizeAndImages ();

		SetSpringJoint ();

		Destroy(gameObject, sentenceInfo.timeInSeconds);
	}

	private void SetCanvasSizeAndImages(){
		Canvas canvas = GetComponentInChildren<Canvas> ();
		int imagesAmount = sentenceInfo.sentence.images.Count;
		if (imagesAmount == 1)
			margin.x = margin.x + extraMarginWidthToOneIcon;
		RectTransform canvasRectTransform = canvas.GetComponent<RectTransform> ();
		canvasRectTransform.sizeDelta = Vector2.Scale (canvasRectTransform.sizeDelta, Vector2.one + 2*margin); 
		canvasRectTransform.sizeDelta = Vector2.Scale (canvasRectTransform.sizeDelta, new Vector2 (imagesAmount,1)); 

		if (sentenceInfo.typeOfBalloon == TypeOfBalloon.Think)
			backgroundImg.sprite = spriteBalloonThink;
		else if (sentenceInfo.typeOfBalloon == TypeOfBalloon.Shout)
			backgroundImg.sprite = spriteBalloonShout;
		else
			backgroundImg.sprite = spriteBalloonNormal;
		
		for (int i = 0; i<imagesAmount; i++) {
			Image img = Instantiate(imgTmp);
			img.rectTransform.SetParent(backgroundImg.rectTransform);
			img.rectTransform.anchorMin = new Vector2(i*(1f/imagesAmount),0f);
			img.rectTransform.anchorMax = new Vector2((i+1)*(1f/imagesAmount),1f);

			img.rectTransform.offsetMin = margin; 
			img.rectTransform.offsetMax = -margin;

			img.sprite = sentenceInfo.sentence.images[i];
			img.gameObject.SetActive(true);
		}

	}

	private void SetSpringJoint(){
		SpringJoint2D springJoint2D = GetComponent<SpringJoint2D> ();
		if (sentenceInfo.Hooker.GetComponent<Rigidbody2D> ()!= null) {
			
			springJoint2D.connectedBody = sentenceInfo.Hooker.GetComponent<Rigidbody2D> ();
			if (hookerRenderer != null)
				springJoint2D.connectedAnchor = new Vector2(springJoint2D.connectedAnchor.x, hookerRenderer.bounds.max.y);

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
