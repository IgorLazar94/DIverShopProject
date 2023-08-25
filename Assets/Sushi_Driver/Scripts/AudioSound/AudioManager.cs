using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource, swimmingSource;
    private bool isSFXPlaying = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(AudioCollection.casualBackMusic);
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogError("Music not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        if (isSFXPlaying)
        {
            Debug.Log("Another sound is already playing. Ignoring new request.");
            return;
        }

        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogError("Sound not found");
        }
        else
        {
            isSFXPlaying = true;
            sfxSource.PlayOneShot(s.clip);
            StartCoroutine(ResetSFXPlayingStatus(s.clip.length));
        }
    }

    private IEnumerator ResetSFXPlayingStatus(float delay)
    {
        yield return new WaitForSeconds(delay);
        isSFXPlaying = false;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}
