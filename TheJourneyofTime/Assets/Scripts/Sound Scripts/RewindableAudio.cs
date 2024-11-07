using UnityEngine;

public class RewindableAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip regularClip;
    public AudioClip reversedClip;

    private bool isReversed = false; // Track if currently playing reversed clip
    private float clipLength;

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

        // Ensure Play on Awake is off to prevent auto-playing
        audioSource.playOnAwake = false;

        // Set up clip length for switching between regular and reversed playback
        if (regularClip != null)
        {
            clipLength = regularClip.length;
        }
        else
        {
            Debug.LogWarning("No regular clip assigned to " + gameObject.name);
        }
    }

    public void PlayRegular()
    {
        if (audioSource == null || regularClip == null) return;

        // Switch to regular playback if currently reversed
        if (isReversed)
        {
            float reversedPosition = audioSource.time;
            float regularPosition = clipLength - reversedPosition;
            audioSource.clip = regularClip;
            audioSource.time = Mathf.Clamp(regularPosition, 0, clipLength);
            isReversed = false;
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.clip = regularClip;
            audioSource.time = 0;
        }

        audioSource.Play();
    }

    public void PlayReversed()
    {
        if (audioSource == null || reversedClip == null) return;

        // Switch to reversed playback if currently regular
        if (!isReversed)
        {
            float regularPosition = audioSource.time;
            float reversedPosition = clipLength - regularPosition;
            audioSource.clip = reversedClip;
            audioSource.time = Mathf.Clamp(reversedPosition, 0, clipLength);
            isReversed = true;
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.clip = reversedClip;
            audioSource.time = 0;
        }

        audioSource.Play();
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
