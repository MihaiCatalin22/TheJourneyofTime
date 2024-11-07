using UnityEngine;
using UnityEngine.Audio;

public class RunningSound : MonoBehaviour
{
    public AudioClip runningClip;
    public AudioSource audioSource;
    public AudioMixerGroup runningMixerGroup;
    private Movement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<Movement>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = runningClip;
        audioSource.outputAudioMixerGroup = runningMixerGroup;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (playerMovement.isRunning && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (!playerMovement.isRunning && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
