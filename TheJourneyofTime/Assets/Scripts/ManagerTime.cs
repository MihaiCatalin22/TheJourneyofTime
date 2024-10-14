using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTime : MonoBehaviour
{
    public static ManagerTime instance;

    public List<TimeObject> timeObjects = new List<TimeObject>();
    
    public float stopDuration = 3f; // Duration for which time remains stopped
    public float cooldownDuration = 5f; // Cooldown period before time can be stopped again
    
    private bool isTimeStopped = false;
    private bool isCooldownActive = false; // Tracks if the cooldown is active

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
        if (Input.GetKeyDown(KeyCode.T) && !isCooldownActive && !isTimeStopped)
        {
            StartCoroutine(TimeStopRoutine());
        }
    }

    private IEnumerator TimeStopRoutine()
    {
        // Stop time for the defined stop duration
        StopTimeForObjects();
        yield return new WaitForSeconds(stopDuration);

        // Resume time after the stop duration
        ResumeTimeForObjects();

        // Start cooldown period
        StartCoroutine(TimeStopCooldown());
    }

    private IEnumerator TimeStopCooldown()
    {
        isCooldownActive = true;
        Debug.Log("Time Stop Cooldown Active");
        
        // Wait for cooldown duration
        yield return new WaitForSeconds(cooldownDuration);
        
        isCooldownActive = false;
        Debug.Log("Time Stop Ready Again");
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
