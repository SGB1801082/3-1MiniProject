using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private List<AudioSource> audioSources;
    private int initialAudioSourceCount = 10;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
        }
        else
        {
            Destroy(gameObject);
        }
    }



    void InitializeAudioSources()
    {
        audioSources = new List<AudioSource>();

        for (int i = 0; i < initialAudioSourceCount; i++)
        {
            GameObject audioObject = new GameObject("AudioSource_" + i);
            audioObject.transform.SetParent(transform);
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();
            audioSources.Add(audioSource);
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        if (clip == null) return;

        AudioSource availableSource = GetAvailableAudioSource();
        if (availableSource != null)
        {
            availableSource.clip = clip;
            availableSource.volume = volume;
            availableSource.Play();
        }
    }

    AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        // If no available AudioSource, create a new one
        GameObject audioObject = new GameObject("AudioSource_" + audioSources.Count);
        audioObject.transform.SetParent(transform);
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSources.Add(audioSource);
        return audioSource;
    }
}
