using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public bool isRewinding = false;
    public int maxPositions = 500000; // Set a fixed max limit for recorded positions
    public float rewindCooldown = 5f;

    private bool isPaused = false;
    private bool canRewind = true;
    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();
    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 savedVelocity;
    private float savedAngularVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (ManagerTime.instance != null)
        {
            ManagerTime.instance.timeObjects.Add(this);
        }
    }

    void Update()
    {
        if (isPaused) return;

        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Record()
    {
        // Ensure the list keeps a maximum of 'maxPositions' entries
        if (positions.Count >= maxPositions)
        {
            positions.RemoveAt(positions.Count - 1);
            rotations.RemoveAt(rotations.Count - 1);
        }

        positions.Insert(0, transform.position);
        rotations.Insert(0, transform.rotation);
    }

    void Rewind()
    {
        if (positions.Count > 0)
        {
            // Move to the recorded position and rotation
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
        if (!canRewind) return;

        isRewinding = true;
        if (rb != null)
        {
            savedVelocity = rb.velocity;
            savedAngularVelocity = rb.angularVelocity;
            rb.isKinematic = true; // Keep it kinematic during rewind
        }

        if (animator != null)
        {
            animator.enabled = false;
        }
    }

    public void StopRewind()
    {
        isRewinding = false;
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = savedVelocity;
            rb.angularVelocity = savedAngularVelocity;
        }

        if (animator != null)
        {
            animator.enabled = true;
        }

        StartCoroutine(RewindCooldown());
    }

    public void PauseTime()
    {
        isPaused = true;
        if (rb != null)
        {
            savedVelocity = rb.velocity;
            savedAngularVelocity = rb.angularVelocity;
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }

        if (animator != null)
        {
            animator.enabled = false;
        }
    }

    public void ResumeTime()
    {
        isPaused = false;
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = savedVelocity;
            rb.angularVelocity = savedAngularVelocity;
        }

        if (animator != null)
        {
            animator.enabled = true;
        }
    }

    IEnumerator RewindCooldown()
    {
        canRewind = false;
        yield return new WaitForSeconds(rewindCooldown);
        canRewind = true;
    }
}
