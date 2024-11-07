using UnityEngine;
using UnityEngine.Audio;

public class CollapsingPlatformSound : MonoBehaviour
{
    public AudioClip collapseClip;
    public AudioClip reverseCollapseClip; 
    public AudioSource collapseAudioSource;
    public AudioMixerGroup collapseMixerGroup;

    private bool isRewinding = false; 

    void Start()
    {
        collapseAudioSource.outputAudioMixerGroup = collapseMixerGroup;
        Debug.Log("CollapsingPlatformSound initialized without playing sound.");
    }

    public void PlayCollapseSound()
    {
        if (!collapseAudioSource.isPlaying)
        {
            collapseAudioSource.clip = isRewinding ? reverseCollapseClip : collapseClip;
            collapseAudioSource.Play();
            Debug.Log(isRewinding ? "Playing reversed collapse sound" : "Playing normal collapse sound");
        }
    }

    public void SetRewindState(bool rewinding)
    {
        if (isRewinding != rewinding) // Only switch if state changes
        {
            isRewinding = rewinding;
            collapseAudioSource.Stop();
            PlayCollapseSound(); 
            Debug.Log(rewinding ? "Set to play reverse collapse" : "Set to play normal collapse");
        }
    }
    
    public void StopSound()
    {
        if (collapseAudioSource.isPlaying)
        {
            collapseAudioSource.Stop();
            Debug.Log("Stopped collapse sound");
        }
    }
}
