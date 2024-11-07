using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LandingSound : MonoBehaviour
{
    public List<AudioClip> landingClips; // List of landing sounds
    public AudioSource landingAudioSource;
    public AudioMixerGroup landingMixerGroup;

    void Start()
    {
        if (landingAudioSource != null)
        {
            landingAudioSource.outputAudioMixerGroup = landingMixerGroup;
            Debug.Log("Landing Audio Source setup complete.");
        }
        else
        {
            Debug.LogError("Landing Audio Source is not assigned!");
        }
    }

    public void PlayLandingSound()
    {
        if (landingAudioSource != null && landingClips.Count > 0)
        {
            AudioClip clipToPlay = landingClips[Random.Range(0, landingClips.Count)];
            landingAudioSource.clip = clipToPlay;
            landingAudioSource.Play();
            Debug.Log("Playing random landing sound: " + clipToPlay.name);
        }
        else
        {
            Debug.LogError("Landing audio source is not assigned or no clips in the list.");
        }
    }
}
