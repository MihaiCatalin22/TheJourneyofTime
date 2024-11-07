using UnityEngine;
using UnityEngine.Audio;

public class FallingSpikeSound : MonoBehaviour
{
    public AudioClip fallingClip;
    public AudioSource fallingAudioSource;
    public AudioMixerGroup fallingMixerGroup;

    void Start()
    {
        if (fallingAudioSource != null)
        {
            fallingAudioSource.outputAudioMixerGroup = fallingMixerGroup;
            fallingAudioSource.clip = fallingClip;
        }
    }

    public void PlayFallingSound()
    {
        if (fallingAudioSource != null && fallingClip != null && !fallingAudioSource.isPlaying)
        {
            fallingAudioSource.Play();
            Debug.Log("Playing Falling Spike Sound");
        }
    }
}
