using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] Sound[] soundEffects;
    [SerializeField] Sound[] musicTracks;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance == this)
            {
                Destroy(this);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            InitializeSounds("SFX", soundEffects);
            InitializeSounds("Music", musicTracks);
        }
    }

    private void InitializeSounds(string prefix, Sound[] sounds)
    {
        foreach (Sound sound in sounds)
        {
            GameObject _go = new GameObject(prefix + " - " + sound.GetName());
            sound.SetSource(_go.AddComponent<AudioSource>());
            _go.transform.parent = transform;
        }
    }

    public void PlaySoundEffect(string name)
    {
        PlaySound(name, soundEffects);
    }

    public void PlayMusic(string name)
    {
        Sound currentTrack = GetCurrentlyPlayingMusic();

        // If there is no music currently playing, play the selection
        if (currentTrack == null)
        {
            PlaySound(name, musicTracks);
            return;
        }

        // If something is already playing, only play the new music if it is a different selection
        if (currentTrack.GetName() != name)
        {
            currentTrack.Stop();
            PlaySound(name, musicTracks);
        }
    }

    private void PlaySound(string name, Sound[] soundArray)
    {
        foreach (Sound sound in soundArray)
        {
            if (sound.GetName() == name)
            {
                sound.Play();
                return;
            }
        }
    }

    public void StopSoundEffect(string name)
    {
        foreach (Sound sound in soundEffects)
        {
            if (sound.GetName() == name && sound.IsPlaying())
            {
                sound.Stop();
                return;
            }
        }
    }

    public Sound GetCurrentlyPlayingMusic()
    {
        foreach (Sound track in musicTracks)
        {
            if (track.IsPlaying())
            {
                return track;
            }
        }

        return null;
    }

    public void StopCurrentlyPlayingMusic()
    {
        foreach (Sound track in musicTracks)
        {
            if (track.IsPlaying())
            {
                track.Stop();
            }
        }
    }

    public void FadeOutCurrentlyPlayingMusic(float duration)
    {
        foreach (Sound track in musicTracks)
        {
            if (track.IsPlaying())
            {
                StartCoroutine(track.FadeOut(duration));
            }
        }
    }
}

[System.Serializable]
public class Sound
{
    [SerializeField] string name;
    [SerializeField] AudioClip audioClip;
    [Range(0f, 1f)]
    [SerializeField] float volume = 1f;
    [Range(-0.5f, 1.4f)]
    [SerializeField] float pitch = 1f;
    [SerializeField] bool loop = false;

    private AudioSource audioSource;

    public string GetName()
    {
        return name;
    }

    public void SetSource(AudioSource _audioSource)
    {
        audioSource = _audioSource;
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = loop;
    }

    public void Play()
    {
        audioSource.Play();
    }
    public void Stop()
    {
        audioSource.Stop();
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    public IEnumerator FadeOut(float duration)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, 0, currentTime / duration);
            yield return null;
        }
        Stop();
        audioSource.volume = start;
        yield break;
    }
}