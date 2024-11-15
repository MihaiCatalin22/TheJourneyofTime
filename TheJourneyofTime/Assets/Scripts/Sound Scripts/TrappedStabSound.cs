using UnityEngine;

public class TrappedStabSound : MonoBehaviour
{
    public AudioSource spikeAudioSource;
    public AudioClip spikeExtendSound;
    public AudioClip reverseSpikeExtendSound;
    public AudioClip[] randomExtendSounds;
    public AudioClip[] reverseRandomExtendSounds;

    private bool isRewinding = false;

    private void Start()
    {
        if (spikeAudioSource == null)
        {
            spikeAudioSource = GetComponent<AudioSource>();
        }

        if (spikeAudioSource == null)
        {
            Debug.LogError("AudioSource component is missing on " + gameObject.name);
        }

        if (spikeExtendSound == null && (randomExtendSounds == null || randomExtendSounds.Length == 0))
        {
            Debug.LogWarning("No spike extend sound assigned on " + gameObject.name);
        }
    }

    public void PlaySpikeExtendSound()
    {
        if (spikeAudioSource == null) return;

        AudioClip[] activeExtendSounds = isRewinding ? reverseRandomExtendSounds : randomExtendSounds;
        AudioClip singleExtendSound = isRewinding ? reverseSpikeExtendSound : spikeExtendSound;

        if (activeExtendSounds != null && activeExtendSounds.Length > 0)
        {
            AudioClip randomClip = activeExtendSounds[Random.Range(0, activeExtendSounds.Length)];
            spikeAudioSource.clip = randomClip;
        }
        else
        {
            spikeAudioSource.clip = singleExtendSound;
        }

        spikeAudioSource.Play();
    }

    public void SetRewindState(bool rewinding)
    {
        isRewinding = rewinding;
    }

    public void StopSound()
    {
        if (spikeAudioSource.isPlaying)
        {
            spikeAudioSource.Stop();
        }
    }
}
