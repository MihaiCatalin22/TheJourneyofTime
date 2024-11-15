using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTime : MonoBehaviour
{
    public static ManagerTime instance;

    public List<TimeObject> timeObjects = new List<TimeObject>();
    public float stopDuration = 3f;
    public float stopCooldownDuration = 5f;
    public float rewindDuration = 10f;
    public float rewindCooldownDuration = 5f;

    public bool isTimeStopped = false;
    public bool isRewinding = false;
    public bool isStopCooldownActive = false;
    public bool isRewindCooldownActive = false;

    public List<AudioSource> activeAudioSources = new List<AudioSource>();
    public TimeStopSound timeStopSound;

    private float currentStopTimer;
    private float currentCooldownTimer;
    private float currentRewindTimer;
    private float currentRewindCooldownTimer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        activeAudioSources.AddRange(FindObjectsOfType<AudioSource>());
        InitializeAudioSources();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isStopCooldownActive && !isTimeStopped)
        {
            StartCoroutine(TimeStopRoutine());
        }

        if (Input.GetKeyDown(KeyCode.E) && !isRewindCooldownActive && !isRewinding)
        {
            StartCoroutine(TimeRewindRoutine());
        }
    }

    private void InitializeAudioSources()
    {
        foreach (var source in activeAudioSources)
        {
            var audioRewind = source.GetComponent<RewindableAudio>();
            if (audioRewind != null)
            {
                audioRewind.PlayRegular();
            }
            else if (source.playOnAwake)
            {
                source.Play();
            }
        }
    }

    private IEnumerator TimeStopRoutine()
    {
        isTimeStopped = true;

        timeStopSound?.PlayTimeStopSound();

        StopTimeForObjects();
        PauseAllSounds();
        currentStopTimer = stopDuration;

        while (currentStopTimer > 0)
        {
            currentStopTimer -= Time.deltaTime;
            yield return null;
        }

        ResumeTimeForObjects();
        timeStopSound?.PlayTimeRestartSound();
        ResumeAllSounds();
        isTimeStopped = false;

        StartCoroutine(TimeStopCooldown());
    }

    private IEnumerator TimeStopCooldown()
    {
        isStopCooldownActive = true;
        currentCooldownTimer = 0;

        while (currentCooldownTimer < stopCooldownDuration)
        {
            currentCooldownTimer += Time.deltaTime;
            yield return null;
        }

        isStopCooldownActive = false;
    }

    private IEnumerator TimeRewindRoutine()
    {
        isRewinding = true;
        currentRewindTimer = rewindDuration;

        StartRewind();
        PlayReversedSoundsImmediately();

        while (currentRewindTimer > 0)
        {
            currentRewindTimer -= Time.deltaTime;
            yield return null;
        }

        StopRewind();
        PlayRegularSoundsImmediately();
        isRewinding = false;

        StartCoroutine(TimeRewindCooldown());
    }

    private void StartRewind()
    {
        foreach (TimeObject obj in timeObjects)
        {
            obj.StartRewind();
        }
    }

    private void StopRewind()
    {
        foreach (TimeObject obj in timeObjects)
        {
            obj.StopRewind();
        }
    }

    private IEnumerator TimeRewindCooldown()
    {
        isRewindCooldownActive = true;
        currentRewindCooldownTimer = 0;

        while (currentRewindCooldownTimer < rewindCooldownDuration)
        {
            currentRewindCooldownTimer += Time.deltaTime;
            yield return null;
        }

        isRewindCooldownActive = false;
    }

    public float GetTimeStopFillAmount()
    {
        if (isTimeStopped)
            return currentStopTimer / stopDuration;
        else if (isStopCooldownActive)
            return currentCooldownTimer / stopCooldownDuration;
        else
            return 1f;
    }

    public float GetRewindFillAmount()
    {
        if (isRewinding)
            return currentRewindTimer / rewindDuration;
        else if (isRewindCooldownActive)
            return currentRewindCooldownTimer / rewindCooldownDuration;
        else
            return 1f;
    }

    public void StopTimeForObjects()
    {
        foreach (TimeObject obj in timeObjects)
        {
            obj.PauseTime();
        }
    }

    public void ResumeTimeForObjects()
    {
        foreach (TimeObject obj in timeObjects)
        {
            obj.ResumeTime();
        }
    }

    private void PauseAllSounds()
    {
        foreach (var source in activeAudioSources)
        {
            if (source != timeStopSound.timeStopAudioSource && source.isPlaying)
            {
                source.Pause();
            }
        }
    }

    private void ResumeAllSounds()
    {
        foreach (var source in activeAudioSources)
        {
            if (source != timeStopSound.timeStopAudioSource && !source.isPlaying)
            {
                source.UnPause();
            }
        }
    }

    private void PlayReversedSoundsImmediately()
    {
        foreach (var source in activeAudioSources)
        {
            var audioRewind = source.GetComponent<RewindableAudio>();
            if (audioRewind != null)
            {
                audioRewind.PlayReversed();
            }
        }
    }

    private void PlayRegularSoundsImmediately()
    {
        foreach (var source in activeAudioSources)
        {
            var audioRewind = source.GetComponent<RewindableAudio>();
            if (audioRewind != null)
            {
                audioRewind.PlayRegular();
            }
        }
    }
}
