using UnityEngine;
using UnityEngine.Audio;

public class CollapsingPlatformSound : MonoBehaviour
{
    public AudioClip collapseClip;
    public AudioSource collapseAudioSource;
    public AudioMixerGroup collapseMixerGroup;

    void Start()
    {
        collapseAudioSource.outputAudioMixerGroup = collapseMixerGroup;
    }

    public void PlayCollapseSound()
    {
        if (!collapseAudioSource.isPlaying)
        {
            collapseAudioSource.clip = collapseClip;
            collapseAudioSource.Play();
        }
    }
}
