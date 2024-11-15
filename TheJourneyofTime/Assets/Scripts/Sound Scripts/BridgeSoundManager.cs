using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSoundManager : MonoBehaviour
{
    [Header("Creaking Settings")]
    public AudioSource creakingAudioSource;
    public AudioClip creakingClip;
    public float creakInterval = 5f;
    public bool isCreakingEnabled = true;

    [Header("Footstep Settings")]
    public AudioSource footstepAudioSource;
    public List<AudioClip> footstepClips = new List<AudioClip>();
    public float footstepInterval = 0.5f;

    private float nextCreakTime;
    private float nextFootstepTime;
    private bool isPlayerOnBridge = false;

    private void Start()
    {
        if (creakingAudioSource != null && creakingClip != null)
        {
            creakingAudioSource.clip = creakingClip;
            creakingAudioSource.loop = false;
        }

        nextCreakTime = Time.time + creakInterval;
    }

    private void Update()
    {
        HandleCreaking();
        HandleFootsteps();
    }

    private void HandleCreaking()
    {
        if (isCreakingEnabled && isPlayerOnBridge && Time.time >= nextCreakTime)
        {
            creakingAudioSource.Play();
            nextCreakTime = Time.time + creakInterval;
        }
    }

    private void HandleFootsteps()
    {
        if (isPlayerOnBridge && Time.time >= nextFootstepTime && footstepClips.Count > 0)
        {
            AudioClip randomFootstep = footstepClips[Random.Range(0, footstepClips.Count)];
            footstepAudioSource.PlayOneShot(randomFootstep);
            nextFootstepTime = Time.time + footstepInterval;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnBridge = true;
            Debug.Log("Player entered bridge.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnBridge = false;
            Debug.Log("Player exited bridge.");
        }
    }
}
