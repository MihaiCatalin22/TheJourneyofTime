using UnityEngine;
using UnityEngine.Audio;

public class TimeRewindSound : MonoBehaviour
{
    public AudioClip timeRewindClip;
    public AudioSource timeRewindAudioSource;
    public AudioMixerGroup timeRewindMixerGroup;

    void Start()
    {
        if (timeRewindAudioSource == null)
        {
            Debug.LogError("Time Rewind Audio Source is not assigned!");
        }
        else
        {
            timeRewindAudioSource.outputAudioMixerGroup = timeRewindMixerGroup;
            Debug.Log("Time Rewind Audio Source setup complete.");
        }
    }

    public void StartRewindSound()
    {
        if (timeRewindAudioSource == null)
        {
            Debug.LogError("Time Rewind Audio Source is missing!");
            return;
        }

        Debug.Log("Attempting to play time rewind sound.");
        if (timeRewindAudioSource.clip == null)
        {
            Debug.LogWarning("Audio clip for Time Rewind is not assigned.");
        }

        if (!timeRewindAudioSource.isPlaying)
        {
            timeRewindAudioSource.clip = timeRewindClip;
            timeRewindAudioSource.Play();
            Debug.Log("Playing time rewind sound.");
        }
        else
        {
            Debug.Log("Time rewind sound is already playing.");
        }
    }

    public void StopRewindSound()
    {
        if (timeRewindAudioSource != null && timeRewindAudioSource.isPlaying)
        {
            timeRewindAudioSource.Stop();
            Debug.Log("Stopped time rewind sound.");
        }
    }
}
