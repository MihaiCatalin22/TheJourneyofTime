using UnityEngine;

public class WaterfallSound : MonoBehaviour
{
    public AudioSource waterfallAudioSource;
    public AudioClip waterfallClip;
    public AudioClip reverseWaterfallClip;

    private bool isRewinding = false;

    void Start()
    {
        if (waterfallAudioSource != null && waterfallClip != null)
        {
            waterfallAudioSource.clip = waterfallClip;
            waterfallAudioSource.loop = true;
            waterfallAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Waterfall Audio Source or Clip is not assigned.");
        }
    }

    public void PlayWaterfallSound()
    {
        if (!waterfallAudioSource.isPlaying)
        {
            waterfallAudioSource.clip = isRewinding ? reverseWaterfallClip : waterfallClip;
            waterfallAudioSource.Play();
        }
    }

    public void StopWaterfallSound()
    {
        if (waterfallAudioSource.isPlaying)
        {
            waterfallAudioSource.Stop();
        }
    }

    public void SetRewindState(bool rewinding)
    {
        isRewinding = rewinding;
        waterfallAudioSource.clip = isRewinding ? reverseWaterfallClip : waterfallClip;
        if (!waterfallAudioSource.isPlaying)
        {
            waterfallAudioSource.Play();
        }
    }
}
