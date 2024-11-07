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

    // Sound References
    public TimeStopSound timeStopSound;
    public TimeRewindSound timeRewindSound;

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
            Debug.Log("Destroying duplicate ManagerTime instance: " + gameObject.name);
            Destroy(gameObject);
        }
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

    private IEnumerator TimeStopRoutine()
    {
        isTimeStopped = true;
        if (timeStopSound != null)
        {
            timeStopSound.PlayTimeStopSound();
            Debug.Log("Playing Time Stop Sound");
        }

        StopTimeForObjects();
        currentStopTimer = stopDuration;

        // Decrease time stop bar while active
        while (currentStopTimer > 0)
        {
            currentStopTimer -= Time.deltaTime;
            yield return null;
        }

        ResumeTimeForObjects();
        isTimeStopped = false;

        // Start cooldown
        StartCoroutine(TimeStopCooldown());
    }

    private IEnumerator TimeStopCooldown()
    {
        isStopCooldownActive = true;
        currentCooldownTimer = 0; // Reset cooldown timer

        // Increment cooldown timer to recharge bar
        while (currentCooldownTimer < stopCooldownDuration)
        {
            currentCooldownTimer += Time.deltaTime;
            yield return null;
        }

        isStopCooldownActive = false;
        Debug.Log("Time Stop Ready Again");
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

    private IEnumerator TimeRewindRoutine()
    {
        isRewinding = true;
        currentRewindTimer = rewindDuration;

        if (timeRewindSound != null)
        {
            timeRewindSound.StartRewindSound();
            Debug.Log("Playing Time Rewind Sound");
        }

        StartRewind();

        // Decrease rewind timer while active
        while (currentRewindTimer > 0)
        {
            currentRewindTimer -= Time.deltaTime;
            yield return null;
        }

        StopRewind();
        isRewinding = false;

        StartCoroutine(TimeRewindCooldown());
    }

    private void StartRewind()
    {
        foreach (TimeObject obj in timeObjects)
        {
            obj.StartRewind();
        }
        Debug.Log("Rewinding Time - Triggering Rewind Sound");

        if (timeRewindSound != null)
        {
            timeRewindSound.StartRewindSound();
        }
    }

    private void StopRewind()
    {
        foreach (TimeObject obj in timeObjects)
        {
            obj.StopRewind();
        }
        Debug.Log("Stopped Rewinding Time - Stopping Rewind Sound");

        if (timeRewindSound != null)
        {
            timeRewindSound.StopRewindSound();
        }
    }

    private IEnumerator TimeRewindCooldown()
    {
        isRewindCooldownActive = true;
        currentRewindCooldownTimer = 0;

        // Increment cooldown timer for rewind
        while (currentRewindCooldownTimer < rewindCooldownDuration)
        {
            currentRewindCooldownTimer += Time.deltaTime;
            yield return null;
        }

        isRewindCooldownActive = false;
        Debug.Log("Time Rewind Ready Again");
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
        Debug.Log("Time has been stopped.");

        if (timeStopSound != null)
        {
            timeStopSound.PlayTimeStopSound();
        }
    }

    public void ResumeTimeForObjects()
    {
        foreach (TimeObject obj in timeObjects)
        {
            obj.ResumeTime();
        }
        Debug.Log("Time has resumed.");

        if (timeStopSound != null)
        {
            timeStopSound.StopTimeStopSound();
        }
    }
}
