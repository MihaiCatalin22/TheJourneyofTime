using UnityEngine;
using UnityEngine.Audio;

public class FastCollapsingPlatformSound : MonoBehaviour
{
    public AudioClip fastCollapseClip;
    public AudioClip reverseFastCollapseClip;
    public AudioSource fastCollapseAudioSource;
    public AudioMixerGroup fastCollapseMixerGroup;

    private bool isRewinding = false;

    void Start()
    {
        fastCollapseAudioSource.outputAudioMixerGroup = fastCollapseMixerGroup;
    }

    public void PlayFastCollapsingSound()
    {
        if (!fastCollapseAudioSource.isPlaying)
        {
            fastCollapseAudioSource.clip = isRewinding ? reverseFastCollapseClip : fastCollapseClip;
            fastCollapseAudioSource.Play();
        }
    }

    public void SetRewindState(bool rewinding)
    {
        if (isRewinding != rewinding)
        {
            isRewinding = rewinding;
            fastCollapseAudioSource.Stop();
            PlayFastCollapsingSound();
        }
    }

    public void StopSound()
    {
        if (fastCollapseAudioSource.isPlaying)
        {
            fastCollapseAudioSource.Stop();
        }
    }
}
