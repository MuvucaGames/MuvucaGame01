using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class Intro : MonoBehaviour
{

    public MovieTexture movie;
    private AudioSource m_audio;
    private Text m_pressStartText;
    private float m_alphaValue;
    private bool m_fadeIn;

    // Use this for initialization
    void Start()
    {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        m_audio = GetComponent<AudioSource>();
        if (movie){
            m_audio.clip = movie.audioClip;
            movie.Play();
            m_audio.Play();
        }
        
        m_alphaValue = 0f;
        m_pressStartText = GameObject.Find("PressStart").GetComponent<Text>();
        m_pressStartText.color = new Color(m_pressStartText.color.r, m_pressStartText.color.g,
                                             m_pressStartText.color.b, m_alphaValue);
        m_fadeIn = true;
    }

    void Update()
    {
        if ((movie && !movie.isPlaying) || !movie)
        {
            StartCoroutine(ShowText());
            if (Input.GetButtonDown("Submit"))
            {
				//TODO
                //Game.LoadLevel(GameLevel.MainMenu);
            }
        }
    }

    private IEnumerator ShowText()
    {
        if (m_fadeIn)
        {
            if (m_alphaValue >= 1)
                m_fadeIn = false;
            while (m_alphaValue < 1)
            {
                m_alphaValue += 0.1f * Time.deltaTime * 0.5f;
                m_pressStartText.color = new Color(m_pressStartText.color.r, m_pressStartText.color.g,
                                                   m_pressStartText.color.b, m_alphaValue);
                yield return null;
            }
        }
        else
        {
            if (m_alphaValue <= 0.25)
                m_fadeIn = true;
            while (m_alphaValue > 0.25)
            {
                m_alphaValue -= 0.1f * Time.deltaTime * 0.5f;
                m_pressStartText.color = new Color(m_pressStartText.color.r, m_pressStartText.color.g,
                                                   m_pressStartText.color.b, m_alphaValue);
                yield return null;
            }
        }
    }
}
