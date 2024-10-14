using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTime : MonoBehaviour
{
    public static ManagerTime instance;

    public List<TimeObject> timeObjects = new List<TimeObject>();

    public float stopDuration = 3f; // Duration for which time remains stopped
    public float stopCooldownDuration = 5f; // Cooldown period before time stop can be reused
    public float rewindDuration = 10f; // Maximum rewind duration
    public float rewindCooldownDuration = 5f; // Cooldown period before rewind can be reused

    private bool isTimeStopped = false;
    private bool isRewinding = false;
    private bool isStopCooldownActive = false;
    private bool isRewindCooldownActive = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Handle time stop activation
        if (Input.GetKeyDown(KeyCode.T) && !isStopCooldownActive && !isTimeStopped)
        {
            StartCoroutine(TimeStopRoutine());
        }

        // Handle time rewind activation
        if (Input.GetKeyDown(KeyCode.R) && !isRewindCooldownActive && !isRewinding)
        {
            StartCoroutine(TimeRewindRoutine());
        }
    }

    private IEnumerator TimeStopRoutine()
    {
        StopTimeForObjects();
        yield return new WaitForSeconds(stopDuration);
        ResumeTimeForObjects();
        StartCoroutine(TimeStopCooldown());
    }

    private IEnumerator TimeStopCooldown()
    {
        isStopCooldownActive = true;
        Debug.Log("Time Stop Cooldown Active");
        yield return new WaitForSeconds(stopCooldownDuration);
        isStopCooldownActive = false;
        Debug.Log("Time Stop Ready Again");
    }

    private IEnumerator TimeRewindRoutine()
    {
        isRewinding = true;
        StartRewind();
        yield return new WaitForSeconds(rewindDuration);
        StopRewind();
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

    private IEnumerator TimeRewindCooldown()
    {
        isRewindCooldownActive = true;
        Debug.Log("Time Rewind Cooldown Active");
        yield return new WaitForSeconds(rewindCooldownDuration);
        isRewindCooldownActive = false;
        Debug.Log("Time Rewind Ready Again");
    }

    public void StopTimeForObjects()
    {
        isTimeStopped = true;
        foreach (TimeObject obj in timeObjects)
        {
            obj.PauseTime();
        }
        Debug.Log("Time has been stopped.");
    }

    public void ResumeTimeForObjects()
    {
        isTimeStopped = false;
        foreach (TimeObject obj in timeObjects)
        {
            obj.ResumeTime();
        }
        Debug.Log("Time has resumed.");
    }
}
