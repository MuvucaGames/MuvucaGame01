using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Dialog : ActionableElement{

	[SerializeField]
	public List<Sentence.Info> senteces;

	public Balloon balloonPrefab;

	private int sentenceCounter = 0;

	private bool alreadyTriggered = false;

	public void Awake(){
		//Remove null objects
		senteces = senteces.Where(item => item != null&&item.sentence!=null).ToList();
	}



	public override void Activate ()
	{
		if (!alreadyTriggered) {
			alreadyTriggered = true;
			NextSentence ();
		}
	}

	public void NextSentence(){
		if(sentenceCounter < senteces.Count){
			Balloon ballonInstance = Instantiate (balloonPrefab);
			ballonInstance.Init (senteces[sentenceCounter], NextSentence);
		}
		sentenceCounter++;
	}

	public override void Deactivate ()
	{

	}


}
