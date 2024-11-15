using UnityEngine;
using System.Collections.Generic;

public class RewindableAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> regularClips = new List<AudioClip>();
    public List<AudioClip> reversedClips = new List<AudioClip>();

    private bool isReversed = false;
    private float clipLength;
    private int currentClipIndex = 0;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on " + gameObject.name);
            return;
        }

        if (regularClips.Count > 0)
        {
            clipLength = regularClips[0].length;
        }
    }

    public void PlayRegular(int index = -1)
    {
        if (audioSource == null || regularClips.Count == 0) return;

        currentClipIndex = (index >= 0 && index < regularClips.Count) ? index : currentClipIndex;

        audioSource.clip = regularClips[currentClipIndex];
        audioSource.time = isReversed ? clipLength - audioSource.time : audioSource.time;
        isReversed = false;

        audioSource.Play();
    }

    public void PlayReversed(int index = -1)
    {
        if (audioSource == null || reversedClips.Count == 0) return;

        currentClipIndex = (index >= 0 && index < reversedClips.Count) ? index : currentClipIndex;

        audioSource.clip = reversedClips[currentClipIndex];
        audioSource.time = !isReversed ? clipLength - audioSource.time : audioSource.time;
        isReversed = true;

        audioSource.Play();
    }

    public void PlayRandomRegular()
    {
        if (regularClips.Count == 0) return;

        int randomIndex = Random.Range(0, regularClips.Count);
        PlayRegular(randomIndex);
    }

    public void PlayRandomReversed()
    {
        if (reversedClips.Count == 0) return;

        int randomIndex = Random.Range(0, reversedClips.Count);
        PlayReversed(randomIndex);
    }

    public void PauseAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public void UnpauseAudio()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }
}
