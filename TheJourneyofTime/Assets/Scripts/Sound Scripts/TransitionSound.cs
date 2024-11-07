using UnityEngine;
using UnityEngine.Audio;

public class TransitionSound : MonoBehaviour
{
    public AudioClip transitionClip; 
    public AudioSource transitionAudioSource;
    public AudioMixerGroup transitionMixerGroup;

    void Start()
    {
        transitionAudioSource.outputAudioMixerGroup = transitionMixerGroup;
    }

    public void PlayTransitionSound()
    {
        if (!transitionAudioSource.isPlaying)
        {
            transitionAudioSource.clip = transitionClip;
            transitionAudioSource.Play();
        }
    }
}
