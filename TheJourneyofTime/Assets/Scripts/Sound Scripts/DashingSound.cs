using UnityEngine;
using UnityEngine.Audio;

public class DashingSound : MonoBehaviour
{
    public AudioClip dashClip;
    public AudioSource dashAudioSource;
    public AudioMixerGroup dashMixerGroup;

    void Start()
    {
        if (dashAudioSource == null)
        {
            Debug.LogError("Dash Audio Source is not assigned!");
            return;
        }

        dashAudioSource.outputAudioMixerGroup = dashMixerGroup;
    }

    public void PlayDashSound()
    {
        if (!dashAudioSource.isPlaying)
        {
            dashAudioSource.clip = dashClip;
            dashAudioSource.Play();
        }
    }
}
