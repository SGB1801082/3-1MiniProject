using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSoundHandler : MonoBehaviour
{
    public AudioClip sceneClip;
    public string targetSceneName;
    [Range(0f, 1f)] public float sceneVolume = 1.0f;

    private AudioSource sceneAudioSource;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == targetSceneName)
        {
            PlaySceneSound(sceneClip, sceneVolume);
        }
        else
        {
            StopSceneSound();
        }
    }

    void PlaySceneSound(AudioClip clip, float volume)
    {
        if (clip == null) return;

        if (sceneAudioSource == null)
        {
            sceneAudioSource = gameObject.AddComponent<AudioSource>();
            sceneAudioSource.loop = true;
        }

        sceneAudioSource.clip = clip;
        sceneAudioSource.volume = volume;
        sceneAudioSource.Play();
    }

    void StopSceneSound()
    {
        if (sceneAudioSource != null)
        {
            sceneAudioSource.Stop();
        }
    }
}
