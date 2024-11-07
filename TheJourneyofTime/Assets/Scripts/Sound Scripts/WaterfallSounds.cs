using UnityEngine;

public class WaterfallSound : MonoBehaviour
{
    public AudioSource waterfallAudioSource;
    public AudioClip waterfallClip;

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
}
