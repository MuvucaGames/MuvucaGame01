using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public class Intro : MonoBehaviour {

	public MovieTexture movie;
	private AudioSource m_audio;

	// Use this for initialization
	void Start () {
		GetComponent<RawImage> ().texture = movie as MovieTexture;
	m_audio = GetComponent<AudioSource> ();
	m_audio.clip = movie.audioClip;
		movie.Play ();
	m_audio.Play ();
	}


	void Update(){
		if (!movie.isPlaying) {
			Game.LoadLevel(GameLevel.MainMenu);
		}
	}
}
