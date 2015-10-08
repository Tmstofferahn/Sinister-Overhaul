using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public static MusicManager musicControl;
    public AudioClip menuMusic;
    public AudioClip[] levelMusic;
    public AudioClip[] bossMusic;
    public AudioSource musicSource;



    void Awake()
    {
        if (musicControl == null)
        {
            musicControl = this;
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
        if (musicControl != null)
        {
            if (musicControl.musicSource != null)
            {
                musicControl.musicSource.Stop();
                musicControl.musicSource.clip = musicControl.menuMusic;
                musicControl.musicSource.Play();
            }
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayGameMusic()
    {
        if (musicControl != null)
        {
            if (musicControl.musicSource != null)
            {
                musicControl.musicSource.Stop();
                musicControl.musicSource.clip = musicControl.levelMusic[Application.loadedLevel];
                musicControl.musicSource.Play();
            }
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

    static public void PlayBossMusic()
    {
        if (musicControl != null)
        {
            if (musicControl.musicSource != null)
            {
                if (musicControl.bossMusic[Application.loadedLevel] != null)
                {
                    musicControl.musicSource.Stop();
                    musicControl.musicSource.clip = musicControl.bossMusic[Application.loadedLevel];
                    musicControl.musicSource.Play();
                }
            }
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }



    public void SetVolume(float val)
	{
        musicControl.musicSource.volume = val;
	}
  

}
