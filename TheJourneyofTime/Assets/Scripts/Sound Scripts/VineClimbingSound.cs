using UnityEngine;
using UnityEngine.Audio;

public class VineClimbingSound : MonoBehaviour
{
    public AudioClip vineClip;
    public AudioSource vineAudioSource;
    public AudioMixerGroup vineMixerGroup;

    void Start()
    {
        vineAudioSource.outputAudioMixerGroup = vineMixerGroup;
    }

    public void PlayVineClimbSound()
    {
        Debug.Log("Attempting to play vine climbing sound.");  // Debugging
        if (!vineAudioSource.isPlaying)
        {
            vineAudioSource.clip = vineClip;
            vineAudioSource.Play();
            Debug.Log("Playing vine climbing sound.");  // Debugging
        }
    }

    public void StopVineClimbSound()  // New method to stop the sound
    {
        if (vineAudioSource.isPlaying)
        {
            vineAudioSource.Stop();
            Debug.Log("Stopping vine climbing sound.");  // Debugging
        }
    }
}
