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
        Debug.Log("TransitionSound initialized without playing sound.");
    }

    public void PlayTransitionSound()
    {
        if (!transitionAudioSource.isPlaying)
        {
            transitionAudioSource.clip = isRewinding ? reverseTransitionClip : transitionClip;
            transitionAudioSource.Play();
            Debug.Log(isRewinding ? "Playing reversed transition sound" : "Playing normal transition sound");
        }
    }

    public void SetRewindState(bool rewinding)
    {
        if (isRewinding != rewinding)
        {
            isRewinding = rewinding;
            transitionAudioSource.Stop();
            Debug.Log(rewinding ? "Set to play reversed transition sound on next play" : "Set to play normal transition sound on next play");
        }
    }

    public void StopSound()
    {
        if (transitionAudioSource.isPlaying)
        {
            transitionAudioSource.Stop();
            Debug.Log("Transition sound stopped.");
        }
    }
}
