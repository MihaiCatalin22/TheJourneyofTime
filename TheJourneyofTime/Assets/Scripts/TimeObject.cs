using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public bool isRewinding = false;
    public float rewindDuration = 5f; // Maximum rewind duration in seconds
    public float stayInPlaceDuration = 2f; // Time to stay in place after rewind ends
    public float fallStartDelay = 3f; // Time to wait before objects start falling

    private bool isPausedAfterRewind = false; // Track whether we are in the stay period
    List<Vector3> positions;
    List<Quaternion> rotations;
    private Rigidbody2D rb; // Rigidbody for controlling physics

    void Start()
    {
        positions = new List<Vector3>();
        rotations = new List<Quaternion>();
        rb = GetComponent<Rigidbody2D>();

        rb.isKinematic = true; // Disable physics at the start
        StartCoroutine(EnableGravityAfterDelay());
    }

    IEnumerator EnableGravityAfterDelay()
    {
        yield return new WaitForSeconds(fallStartDelay); // Wait before enabling gravity
        rb.isKinematic = false; // Enable physics after the delay
    }

    void Update()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else if (!isPausedAfterRewind)
        {
            Record();
        }
    }

    void Record()
    {
        // Record only the last 5 seconds of positions
        if (positions.Count > Mathf.Round(rewindDuration / Time.fixedDeltaTime))
        {
            positions.RemoveAt(positions.Count - 1);
            rotations.RemoveAt(positions.Count - 1);
        }

        positions.Insert(0, transform.position);
        rotations.Insert(0, transform.rotation);
    }

    void Rewind()
    {
        if (positions.Count > 0)
        {
            transform.position = positions[0];
            transform.rotation = rotations[0];

            positions.RemoveAt(0);
            rotations.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true; // Disable physics
    }

    public void StopRewind()
    {
        isRewinding = false;
        StartCoroutine(PauseAfterRewind()); // Pause after rewinding
    }

    IEnumerator PauseAfterRewind()
    {
        isPausedAfterRewind = true;
        yield return new WaitForSeconds(stayInPlaceDuration); // Stay in place for a few seconds
        rb.isKinematic = false; // Re-enable physics after the pause
        isPausedAfterRewind = false;
    }
}