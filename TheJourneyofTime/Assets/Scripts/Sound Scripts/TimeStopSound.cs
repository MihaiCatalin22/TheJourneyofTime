using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class TimeStopSound : MonoBehaviour
{
    public AudioClip timeStopClip;
    public AudioClip timeRestartClip;
    public AudioSource timeStopAudioSource;
    public AudioMixerGroup timeStopMixerGroup;

    private void Start()
    {
        if (timeStopAudioSource == null)
        {
            Debug.LogError("Time Stop Audio Source is not assigned!");
        }
        else
        {
            Debug.Log("Time Stop Audio Source setup complete.");
        }
    }

    private void Update()
    {
        if (timeStopAudioSource != null)
        {
            Debug.Log($"Time Stop AudioSource Playing State: {timeStopAudioSource.isPlaying}");
        }
    }

    public void PlayTimeStopSound()
{
    if (timeStopAudioSource != null && timeStopClip != null)
    {
        timeStopAudioSource.clip = timeStopClip;
        timeStopAudioSource.volume = 1f;
        timeStopAudioSource.Play();
    }
}

    public void PlayTimeRestartSound()
    {
        if (timeStopAudioSource != null && timeRestartClip != null)
        {
            timeStopAudioSource.outputAudioMixerGroup = timeStopMixerGroup;
            timeStopAudioSource.clip = timeRestartClip;
            timeStopAudioSource.volume = 1f;
            timeStopAudioSource.Play();  
        }
        else
        {
            Debug.LogError("Time Restart Clip or AudioSource is missing.");
        }
    }

    private IEnumerator LogPlaybackTime(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
