using System.Collections.Generic;
using UnityEngine;

public class ManagerTime : MonoBehaviour
{
    public static ManagerTime instance;

    public List<TimeObject> timeObjects = new List<TimeObject>();
    private bool isTimeStopped = false;

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
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isTimeStopped)
            {
                ResumeTimeForObjects();
            }
            else
            {
                StopTimeForObjects();
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            StartRewind();
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            StopRewind();
        }
    }

    public void StartRewind()
    {
        foreach (TimeObject obj in timeObjects)
        {
            obj.StartRewind();
        }
    }

    public void StopRewind()
    {
        foreach (TimeObject obj in timeObjects)
        {
            obj.StopRewind();
        }
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
