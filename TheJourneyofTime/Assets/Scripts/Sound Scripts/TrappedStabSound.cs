using UnityEngine;

public class TrappedStabSound : MonoBehaviour
{
    public AudioSource spikeAudioSource;
    public AudioClip spikeExtendSound;
    public AudioClip[] randomExtendSounds;

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

        if (randomExtendSounds != null && randomExtendSounds.Length > 0)
        {
            AudioClip randomClip = randomExtendSounds[Random.Range(0, randomExtendSounds.Length)];
            spikeAudioSource.clip = randomClip;
        }
        else
        {
            spikeAudioSource.clip = spikeExtendSound;
        }

        spikeAudioSource.Play();
        Debug.Log("Playing spike extend sound");
    }
}
