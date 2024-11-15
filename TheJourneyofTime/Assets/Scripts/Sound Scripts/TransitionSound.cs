using UnityEngine;
using UnityEngine.Audio;

public class TransitionSound : MonoBehaviour
{
    public AudioClip transitionClip;
    public AudioClip reverseTransitionClip;
    public AudioSource transitionAudioSource;
    public AudioMixerGroup transitionMixerGroup;

    private bool isRewinding = false;

    void Start()
    {
        transitionAudioSource.outputAudioMixerGroup = transitionMixerGroup;
    }

    public void PlayTransitionSound()
    {
        if (!transitionAudioSource.isPlaying)
        {
            transitionAudioSource.clip = isRewinding ? reverseTransitionClip : transitionClip;
            transitionAudioSource.Play();
        }
    }

    public void SetRewindState(bool rewinding)
    {
        if (isRewinding != rewinding)
        {
            isRewinding = rewinding;
            transitionAudioSource.Stop();
        }
    }

    public void StopSound()
    {
        if (transitionAudioSource.isPlaying)
        {
            transitionAudioSource.Stop();
        }
    }
}
