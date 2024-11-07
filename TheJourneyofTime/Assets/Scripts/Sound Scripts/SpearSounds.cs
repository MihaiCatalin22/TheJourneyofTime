using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSounds : MonoBehaviour
{
    public List<AudioClip> spearOutClips; 
    public AudioClip spearInClip; 
    public AudioSource audioSource;   

    private bool isSpearOut = false;    

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
        if (!isSpearOut && spearOutClips.Count > 0)
        {
            audioSource.clip = spearOutClips[Random.Range(0, spearOutClips.Count)];
            audioSource.Play();
            Debug.Log("Playing Spear Going Out Sound");
            isSpearOut = true;
        }
    }

    public void PlaySpearInSound()
    {
        if (isSpearOut) 
        {
            audioSource.clip = spearInClip;
            audioSource.Play();
            Debug.Log("Playing Spear Going In Sound");
            isSpearOut = false;
        }
    }
}
