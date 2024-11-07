using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class TimeStopSound : MonoBehaviour
{
    public AudioClip timeStopClip;
    public AudioSource timeStopAudioSource;
    public AudioMixerGroup timeStopMixerGroup;

    void Start()
    {
        if (timeStopAudioSource == null)
        {
            Debug.LogError("Time Stop Audio Source is not assigned!");
        }
        else
        {
            timeStopAudioSource.outputAudioMixerGroup = timeStopMixerGroup;
            Debug.Log("Time Stop Audio Source setup complete.");
        }
    }
    public IEnumerator PlayWithDelay(AudioSource source, AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.clip = clip;
        source.Play();
        Debug.Log("Played sound with delay");
    }
    public void PlayTimeStopSound()
{
    Debug.Log("Attempting to play time stop sound.");
    timeStopAudioSource.PlayOneShot(timeStopClip);
    Debug.Log("Played time stop sound with PlayOneShot.");
}



    public void StopTimeStopSound()
    {
        if (timeStopAudioSource != null && timeStopAudioSource.isPlaying)
        {
            timeStopAudioSource.Stop();
            Debug.Log("Stopped time stop sound.");
        }
    }
}