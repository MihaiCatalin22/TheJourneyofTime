using System.Collections.Generic;
using UnityEngine;

public class SpearSounds : MonoBehaviour
{
    public List<AudioClip> spearOutClips; 
    public List<AudioClip> reverseSpearOutClips;
    public AudioClip spearInClip;
    public AudioClip reverseSpearInClip;
    public AudioSource audioSource;

    private bool isSpearOut = false;
    private bool isRewinding = false;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
            audioSource.loop = false;
        }
    }

    public void PlaySpearOutSound()
    {
        if (!isSpearOut && (spearOutClips.Count > 0 || reverseSpearOutClips.Count > 0))
        {
            List<AudioClip> activeClipList = isRewinding ? reverseSpearOutClips : spearOutClips;
            audioSource.clip = activeClipList[Random.Range(0, activeClipList.Count)];
            audioSource.Play();
            isSpearOut = true;
        }
    }

    public void PlaySpearInSound()
    {
        if (isSpearOut)
        {
            audioSource.clip = isRewinding ? reverseSpearInClip : spearInClip;
            audioSource.Play();
            isSpearOut = false;
        }
    }

    public void SetRewindState(bool rewinding)
    {
        isRewinding = rewinding;
    }

    public void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
