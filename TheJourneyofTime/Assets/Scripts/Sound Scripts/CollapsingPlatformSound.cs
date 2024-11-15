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
    }

    public void PlayCollapseSound()
    {
        if (!collapseAudioSource.isPlaying)
        {
            collapseAudioSource.clip = isRewinding ? reverseCollapseClip : collapseClip;
            collapseAudioSource.Play();
        }
    }

    public void SetRewindState(bool rewinding)
    {
        if (isRewinding != rewinding) 
        {
            isRewinding = rewinding;
            collapseAudioSource.Stop();
            PlayCollapseSound(); 
        }
    }
    
    public void StopSound()
    {
        if (collapseAudioSource.isPlaying)
        {
            collapseAudioSource.Stop();
        }
    }
}
