using System.Collections.Generic;
using UnityEngine;

public class RockImpactSound : MonoBehaviour
{
    public AudioSource impactAudioSource;
    public List<AudioClip> impactClips = new List<AudioClip>();

    public void PlayImpactSound()
    {
        if (impactClips.Count > 0)
        {
            AudioClip randomImpactClip = impactClips[Random.Range(0, impactClips.Count)];
            impactAudioSource.PlayOneShot(randomImpactClip);
            Debug.Log("Playing Rock Impact Sound");
        }
    }
}
