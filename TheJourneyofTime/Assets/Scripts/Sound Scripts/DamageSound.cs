using UnityEngine;
using UnityEngine.Audio;

public class DamageSound : MonoBehaviour
{
    public AudioClip damageClip;
    public AudioSource damageAudioSource;
    public AudioMixerGroup damageMixerGroup;

    void Start()
    {
        damageAudioSource.outputAudioMixerGroup = damageMixerGroup;
    }

    public void PlayDamageSound()
    {
        Debug.Log("Attempting to play damage sound.");  // Debugging
        damageAudioSource.PlayOneShot(damageClip);
        Debug.Log("Playing damage sound.");  // Debugging
    }
}
