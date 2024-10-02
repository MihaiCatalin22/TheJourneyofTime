using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTime : MonoBehaviour
{
    public static ManagerTime instance;

    public List<TimeObject> timeObjects = new List<TimeObject>();
    private bool isTimeStopped = false;
    void Start()
    {
        Application.targetFrameRate = 60;
    }
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
                ResumeTime();
            }
            else
            {
                StopTime();
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

    public void StopTime()
    {
        Time.timeScale = 0f;
        isTimeStopped = true;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
        isTimeStopped = false;
    }
}