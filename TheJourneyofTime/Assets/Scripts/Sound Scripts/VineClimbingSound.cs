using UnityEngine;
using UnityEngine.Audio;

public class VineClimbingSound : MonoBehaviour
{
    public AudioClip vineClip;
    public AudioClip reverseVineClip; // Reversed version for rewind
    public AudioSource vineAudioSource;
    public AudioMixerGroup vineMixerGroup;

    private bool isRewinding = false;

    void Start()
    {
        vineAudioSource.outputAudioMixerGroup = vineMixerGroup;
        Debug.Log("VineClimbingSound initialized without playing sound.");
    }

    public void PlayVineClimbSound()
    {
        if (!vineAudioSource.isPlaying)
        {
            vineAudioSource.clip = isRewinding ? reverseVineClip : vineClip;
            vineAudioSource.Play();
            Debug.Log(isRewinding ? "Playing reversed vine climbing sound" : "Playing normal vine climbing sound");
        }
    }

    public void StopVineClimbSound()
    {
        if (vineAudioSource.isPlaying)
        {
            vineAudioSource.Stop();
            Debug.Log("Vine climbing sound stopped.");
        }
    }

    public void SetRewindState(bool rewinding)
    {
        if (isRewinding != rewinding)
        {
            isRewinding = rewinding;
            vineAudioSource.Stop();
            Debug.Log(rewinding ? "Set to play reversed vine sound on next play" : "Set to play normal vine sound on next play");
        }
    }
}
