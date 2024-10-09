using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;   // Singleton instance
    public AudioSource musicSource;        // The audio source for music

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Avoid duplicates
        }
    }

    public void ToggleMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            PlayerPrefs.SetInt("MusicOn", 0); // Save preference
        }
        else
        {
            musicSource.Play();
            PlayerPrefs.SetInt("MusicOn", 1); // Save preference
        }
    }

    // Load saved preferences at the start
    void Start()
    {
        if (PlayerPrefs.GetInt("MusicOn", 1) == 1) // Default to on
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Pause();
        }
    }
}
