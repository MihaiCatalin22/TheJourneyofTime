using System.Collections.Generic;
using UnityEngine;

public class RockImpactSound : MonoBehaviour
{
    public AudioSource impactAudioSource;
    public List<AudioClip> impactClips = new List<AudioClip>();
    public List<AudioClip> reverseImpactClips = new List<AudioClip>();

    private bool isRewinding = false;

    public void PlayImpactSound()
    {
        if (impactClips.Count > 0 || reverseImpactClips.Count > 0)
        {
            List<AudioClip> activeClipList = isRewinding ? reverseImpactClips : impactClips;
            AudioClip randomImpactClip = activeClipList[Random.Range(0, activeClipList.Count)];
            impactAudioSource.PlayOneShot(randomImpactClip);
        }
    }

    public void SetRewindState(bool rewinding)
    {
        isRewinding = rewinding;
    }

    public void StopSound()
    {
        if (impactAudioSource.isPlaying)
        {
            impactAudioSource.Stop();
        }
    }
}
