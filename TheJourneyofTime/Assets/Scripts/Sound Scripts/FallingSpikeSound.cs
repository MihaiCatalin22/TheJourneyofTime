using UnityEngine;
using UnityEngine.Audio;

public class FallingSpikeSound : MonoBehaviour
{
    public AudioClip fallingClip;
    public AudioClip reverseFallingClip;
    public AudioSource fallingAudioSource;
    public AudioMixerGroup fallingMixerGroup;

    private bool isRewinding = false;

    void Start()
    {
        if (fallingAudioSource != null)
        {
            fallingAudioSource.outputAudioMixerGroup = fallingMixerGroup;
            fallingAudioSource.clip = fallingClip;
        }
    }

    public void PlayFallingSound()
    {
        if (fallingAudioSource != null && !fallingAudioSource.isPlaying)
        {
            fallingAudioSource.clip = isRewinding ? reverseFallingClip : fallingClip;
            fallingAudioSource.Play();
            Debug.Log("Playing Falling Spike Sound");
        }
    }

    public void SetRewindState(bool rewinding)
    {
        isRewinding = rewinding;
        if (fallingAudioSource.isPlaying)
        {
            fallingAudioSource.Stop();
            PlayFallingSound();
        }
    }

    public void StopSound()
    {
        if (fallingAudioSource.isPlaying)
        {
            fallingAudioSource.Stop();
        }
    }
}
