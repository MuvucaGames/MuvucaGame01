using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Sentence : ScriptableObject
{
	public List<Sprite> images;

	[System.Serializable]
	public class Info{
		public Sentence sentence;
		
		public int timeInSeconds = 2;
		
		public Transform Hooker;
		
		public Balloon.TypeOfBalloon typeOfBalloon;
		
	}
}

