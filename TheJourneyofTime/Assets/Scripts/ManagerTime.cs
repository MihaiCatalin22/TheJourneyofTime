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
        if (timeStopSound != null)
        {
            timeStopSound.PlayTimeStopSound();
            Debug.Log("Playing Time Stop Sound");  // Debug for Time Stop Sound
        }

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
        if (timeRewindSound != null)
        {
            timeRewindSound.StartRewindSound();
            Debug.Log("Playing Time Rewind Sound");  // Debug for Time Rewind Sound
        }

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
        Debug.Log("Rewinding Time - Triggering Rewind Sound");  // Debugging
        
        if (timeRewindSound != null)
        {
            timeRewindSound.StartRewindSound();
        }
    }

    private void StopRewind()
    {
        isRewinding = false;
        foreach (TimeObject obj in timeObjects)
        {
            obj.StopRewind();
        }
        Debug.Log("Stopped Rewinding Time - Stopping Rewind Sound");  // Debugging
        
        if (timeRewindSound != null)
        {
            timeRewindSound.StopRewindSound();
        }
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
        Debug.Log("Time Stopped - Playing Time Stop Sound");  // Debugging

        if (timeStopSound != null)
        {
            timeStopSound.PlayTimeStopSound();
        }
    }

    public void ResumeTimeForObjects()
    {
        isTimeStopped = false;
        foreach (TimeObject obj in timeObjects)
        {
            obj.ResumeTime();
        }
        Debug.Log("Time Resumed - Stopping Time Stop Sound");  // Debugging

        if (timeStopSound != null)
        {
            timeStopSound.StopTimeStopSound();
        }
    }
}
