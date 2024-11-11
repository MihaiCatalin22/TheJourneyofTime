using UnityEngine;

public class WaterfallSound : MonoBehaviour
{
    public AudioSource waterfallAudioSource;
    public AudioClip waterfallClip;
    public AudioClip reverseWaterfallClip; // Reversed version for rewind effect

    private bool isRewinding = false;

    void Start()
    {
        if (waterfallAudioSource != null && waterfallClip != null)
        {
            waterfallAudioSource.clip = waterfallClip;
            waterfallAudioSource.loop = true;
            waterfallAudioSource.Play();
            Debug.Log("Waterfall Sound Playing");
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
            Debug.Log(isRewinding ? "Playing reversed waterfall sound" : "Playing normal waterfall sound");
        }
    }

    public void StopWaterfallSound()
    {
        if (waterfallAudioSource.isPlaying)
        {
            waterfallAudioSource.Stop();
            Debug.Log("Waterfall sound stopped.");
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
