using UnityEngine;
using UnityEngine.Audio;

public class FastCollapsingPlatformSound : MonoBehaviour
{
    public AudioClip fastCollapseClip;
    public AudioSource fastCollapseAudioSource;
    public AudioMixerGroup fastCollapseMixerGroup;
    void Start()
    {
        fastCollapseAudioSource.outputAudioMixerGroup = fastCollapseMixerGroup;
    }
    public void PlayFastCollapsingSound()
    {
       if (!fastCollapseAudioSource.isPlaying)
        {
            fastCollapseAudioSource.clip = fastCollapseClip;
            fastCollapseAudioSource.Play();
        }
    }
}
