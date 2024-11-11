using UnityEngine;
using UnityEngine.Audio;

public class VineClimbingSound : MonoBehaviour
{
    public AudioClip vineClip;
    public AudioClip reverseVineClip;
    public AudioSource vineAudioSource;
    public AudioMixerGroup vineMixerGroup;

    private bool isRewinding = false;

    void Start()
    {
        vineAudioSource.outputAudioMixerGroup = vineMixerGroup;
    }

    public void PlayVineClimbSound()
    {
        if (!vineAudioSource.isPlaying)
        {
            vineAudioSource.clip = isRewinding ? reverseVineClip : vineClip;
            vineAudioSource.Play();
        }
    }

    public void StopVineClimbSound()
    {
        if (vineAudioSource.isPlaying)
        {
            vineAudioSource.Stop();
        }
    }

    public void SetRewindState(bool rewinding)
    {
        if (isRewinding != rewinding)
        {
            isRewinding = rewinding;
            vineAudioSource.Stop();
        }
    }
}
