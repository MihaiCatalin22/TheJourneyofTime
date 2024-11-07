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
        Debug.Log("FastCollapsingPlatformSound initialized without playing sound.");
    }

    public void PlayFastCollapsingSound()
    {
        if (!fastCollapseAudioSource.isPlaying)
        {
            fastCollapseAudioSource.clip = isRewinding ? reverseFastCollapseClip : fastCollapseClip;
            fastCollapseAudioSource.Play();
            Debug.Log(isRewinding ? "Playing reversed fast collapse sound" : "Playing normal fast collapse sound");
        }
    }

    public void SetRewindState(bool rewinding)
    {
        if (isRewinding != rewinding)
        {
            isRewinding = rewinding;
            fastCollapseAudioSource.Stop();
            PlayFastCollapsingSound();
            Debug.Log(rewinding ? "Set to play reverse fast collapse" : "Set to play normal fast collapse");
        }
    }

    public void StopSound()
    {
        if (fastCollapseAudioSource.isPlaying)
        {
            fastCollapseAudioSource.Stop();
            Debug.Log("Stopped fast collapse sound");
        }
    }
}
