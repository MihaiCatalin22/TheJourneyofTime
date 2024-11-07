using UnityEngine;
using UnityEngine.Audio;

public class JumpingSound : MonoBehaviour
{
    public AudioClip jumpClip;
    public AudioSource jumpAudioSource;
    public AudioMixerGroup jumpMixerGroup;

    void Start()
    {
        jumpAudioSource.outputAudioMixerGroup = jumpMixerGroup;
    }

    public void PlayJumpSound()
    {
        if (!jumpAudioSource.isPlaying)
        {
            jumpAudioSource.clip = jumpClip;
            jumpAudioSource.Play();
        }
    }
}
