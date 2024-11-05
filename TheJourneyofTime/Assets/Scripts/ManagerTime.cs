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

    // private IEnumerator TimeStopRoutine()
    // {
    //     StopTimeForObjects();
    //     yield return new WaitForSeconds(stopDuration);
    //     ResumeTimeForObjects();
    //     StartCoroutine(TimeStopCooldown());
    // }

    private IEnumerator TimeStopRoutine()
    {
        isTimeStopped = true;
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

    // private IEnumerator TimeStopCooldown()
    // {
    //     isStopCooldownActive = true;
    //     Debug.Log("Time Stop Cooldown Active");
    //     yield return new WaitForSeconds(stopCooldownDuration);
    //     isStopCooldownActive = false;
    //     Debug.Log("Time Stop Ready Again");
    // }
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

    // private IEnumerator TimeRewindRoutine()
    // {
    //     isRewinding = true;
    //     StartRewind();
    //     yield return new WaitForSeconds(rewindDuration);
    //     StopRewind();
    // }
    private IEnumerator TimeRewindRoutine()
    {
        isRewinding = true;
        currentRewindTimer = rewindDuration;

        while (currentRewindTimer > 0)
        {
            currentRewindTimer -= Time.deltaTime;
            yield return null;
        }

        isRewinding = false;

        StartCoroutine(TimeRewindCooldown());
    }

    private void StartRewind()
    {
        foreach (TimeObject obj in timeObjects)
        {
            obj.StartRewind();
        }
        Debug.Log("Rewinding Time");
    }

    private void StopRewind()
    {
        isRewinding = false;
        foreach (TimeObject obj in timeObjects)
        {
            obj.StopRewind();
        }
        Debug.Log("Stopped Rewinding Time");
        StartCoroutine(TimeRewindCooldown());
    }

    // private IEnumerator TimeRewindCooldown()
    // {
    //     isRewindCooldownActive = true;
    //     Debug.Log("Time Rewind Cooldown Active");
    //     yield return new WaitForSeconds(rewindCooldownDuration);
    //     isRewindCooldownActive = false;
    //     Debug.Log("Time Rewind Ready Again");
    // }

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

    public float GetRewindFillAmount()
    {
        if (isRewinding)
            return currentRewindTimer / rewindDuration;
        else if (isRewindCooldownActive)
            return currentRewindCooldownTimer / rewindCooldownDuration;
        else
            return 1f;
    }

    // public void StopTimeForObjects()
    // {
    //     isTimeStopped = true;
    //     foreach (TimeObject obj in timeObjects)
    //     {
    //         obj.PauseTime();
    //     }
    //     Debug.Log("Time has been stopped.");
    // }
    public void StopTimeForObjects()
    {
        foreach (TimeObject obj in timeObjects)
        {
            obj.PauseTime();
        }
        Debug.Log("Time has been stopped.");
    }
    
    // public void ResumeTimeForObjects()
    // {
    //     isTimeStopped = false;
    //     foreach (TimeObject obj in timeObjects)
    //     {
    //         obj.ResumeTime();
    //     }
    //     Debug.Log("Time has resumed.");
    // }
    public void ResumeTimeForObjects()
    {
        foreach (TimeObject obj in timeObjects)
        {
            obj.ResumeTime();
        }
        Debug.Log("Time has resumed.");
    }
}
