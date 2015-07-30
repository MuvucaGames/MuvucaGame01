using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public class Intro : MonoBehaviour {

	public MovieTexture movie;
	AudioSource audio;

	// Use this for initialization
	void Start () {
        //temporary as I cant import the movie
        StartCoroutine(LoadMainAfterTime(5));

        GetComponent<RawImage>().texture = movie as MovieTexture;
		audio = GetComponent<AudioSource> ();
		audio.clip = movie.audioClip;
		movie.Play ();
		audio.Play ();
    }

	void Update(){
		if (!movie.isPlaying) {
			Application.LoadLevel("MainMenu");
		}
	}

    //temporary as I cant import the movie
    IEnumerator LoadMainAfterTime(int secs)
    {
        yield return new WaitForSeconds(secs);
        Application.LoadLevel("MainMenu");
        yield break;
    }
}
