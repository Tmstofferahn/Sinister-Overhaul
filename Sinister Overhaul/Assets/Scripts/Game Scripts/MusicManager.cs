using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public AudioClip menuMusic;
    public AudioClip[] levelMusic;
    public AudioSource musicSource;

    public static MusicManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        PlayMenuMusic();
    }

    static public void PlayMenuMusic()
    {
        if (instance != null)
        {
            if (instance.musicSource != null)
            {
                instance.musicSource.Stop();
                instance.musicSource.clip = instance.menuMusic;
                instance.musicSource.Play();
            }
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayGameMusic()
    {
        if (instance != null)
        {
            if (instance.musicSource != null)
            {
                instance.musicSource.Stop();
                instance.musicSource.clip = instance.levelMusic[Application.loadedLevel];
                instance.musicSource.Play();
            }
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }


    public void SetVolume(float val)
	{
		instance.musicSource.volume = val;
	}
  

}
