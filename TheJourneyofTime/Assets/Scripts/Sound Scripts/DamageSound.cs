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
        damageAudioSource.PlayOneShot(damageClip);
    }
}
