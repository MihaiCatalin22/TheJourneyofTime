using UnityEngine;
using System.Collections.Generic;

public class RewindableAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> regularClips = new List<AudioClip>();
    public List<AudioClip> reversedClips = new List<AudioClip>();

    private bool isReversed = false; // Track if currently playing reversed clip
    private float clipLength;
    private int currentClipIndex = 0; // Index to track which sound to play

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on " + gameObject.name);
        }

        // Check if there are clips in the list
        if (regularClips.Count == 0 || reversedClips.Count == 0)
        {
            Debug.LogWarning("No regular or reversed clips assigned on " + gameObject.name);
            return;
        }

        // Assume the first clip's length for consistency
        clipLength = regularClips[0].length;
    }

    public void PlayRegular(int index = -1)
    {
        if (audioSource == null || regularClips.Count == 0) return;

        // Set the clip based on the provided index, or play the current one if index is -1
        currentClipIndex = (index >= 0 && index < regularClips.Count) ? index : currentClipIndex;

        audioSource.clip = regularClips[currentClipIndex];
        audioSource.time = isReversed ? clipLength - audioSource.time : audioSource.time;
        isReversed = false;

        audioSource.Play();
        Debug.Log($"Playing regular clip '{audioSource.clip.name}' at index {currentClipIndex}");
    }

    public void PlayReversed(int index = -1)
    {
        if (audioSource == null || reversedClips.Count == 0) return;

        currentClipIndex = (index >= 0 && index < reversedClips.Count) ? index : currentClipIndex;

        audioSource.clip = reversedClips[currentClipIndex];
        audioSource.time = !isReversed ? clipLength - audioSource.time : audioSource.time;
        isReversed = true;

        audioSource.Play();
        Debug.Log($"Playing reversed clip '{audioSource.clip.name}' at index {currentClipIndex}");
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
            Debug.Log("Audio paused for " + gameObject.name);
        }
    }

    public void UnpauseAudio()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.UnPause();
            Debug.Log("Audio unpaused for " + gameObject.name);
        }
    }
}
