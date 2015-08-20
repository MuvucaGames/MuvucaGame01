using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    private bool isSFXEnabled, isMusicEnabled;
    public bool IsSFXEnabled
    {
        get
        {
            return isSFXEnabled;
        }
        set
        {
            isSFXEnabled = value;
        }
    }
    public bool IsMusicEnabled
    {
        get
        {
            return isMusicEnabled;
        }
        set
        {
            isMusicEnabled = value;
        }
    }

    private float musicVolume;
    private float sfxVolume;

    private AudioSource source;
    private AudioClip music;
    private AudioClip sfxSwap, sfxWater, sfxOpenGate, sfxCloseGate, sfxJump, sfxReset;

	void Awake () {
        instance = this;

        isSFXEnabled = true;
        isMusicEnabled = true;

        source = GetComponent<AudioSource>();
        
        musicVolume = 0.8f;
        sfxVolume = 0.9f;

        ChangeMusic();
        if (isMusicEnabled)
            PlayMusic();

        string sfxPath = "Sounds/SFX/";       
        sfxJump = Resources.Load<AudioClip>(sfxPath + "jump");
        sfxCloseGate = Resources.Load<AudioClip>(sfxPath + "closeGate");
        sfxOpenGate = Resources.Load<AudioClip>(sfxPath + "openGate");
        sfxReset = Resources.Load<AudioClip>(sfxPath + "reset");
        sfxSwap = Resources.Load<AudioClip>(sfxPath + "swap");
        sfxWater = Resources.Load<AudioClip>(sfxPath + "water");
	}

    private void PlayMusic()
    {
        source.loop = true;
        source.clip = music;
        source.volume = musicVolume;
        source.Play();
    }

    private void ChangeMusic()
    {
        string musicPath = "Sounds/Music/" + Application.loadedLevelName + "Music";
        music = Resources.Load<AudioClip>(musicPath);
    }

    private void PlaySFXSwap()
    {
        source.PlayOneShot(sfxSwap, sfxVolume);
    }

    private void PlaySFXWater()
    {
        source.PlayOneShot(sfxWater, sfxVolume);
    }

    private void PlaySFXOpenGate()
    {
        source.PlayOneShot(sfxOpenGate, sfxVolume);
    }

    private void PlaySFXCloseGate()
    {
        source.PlayOneShot(sfxCloseGate, sfxVolume);
    }

    private void PlaySFXJump()
    {
        source.PlayOneShot(sfxJump, sfxVolume);
    }

    private void PlaySFXReset()
    {
        source.PlayOneShot(sfxReset, sfxVolume);
    }
}
