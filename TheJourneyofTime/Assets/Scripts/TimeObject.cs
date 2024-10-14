using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public bool isRewinding = false;
    public float rewindDuration = 5f;
    public float stayInPlaceDuration = 2f;
    public float rewindCooldown = 5f;

    private bool isPaused = false;
    private bool canRewind = true;
    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();
    private Rigidbody2D rb;
    private Animator animator; // Optional if using an animator

    // Store velocities to resume later
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
        // Store position and rotation for rewinding
        if (positions.Count > Mathf.Round(rewindDuration / Time.fixedDeltaTime))
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
        if (canRewind)
        {
            isRewinding = true;
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }

    public void StopRewind()
    {
        isRewinding = false;
        StartCoroutine(RewindCooldown());
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    public void PauseTime()
    {
        isPaused = true;

        // Stop movement and save velocity if there's a Rigidbody
        if (rb != null)
        {
            savedVelocity = rb.velocity;
            savedAngularVelocity = rb.angularVelocity;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.isKinematic = true; // Freezes the Rigidbody to stop physics
        }

        // Optionally stop animations
        if (animator != null)
        {
            animator.enabled = false;
        }

        Debug.Log(gameObject.name + " paused.");
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

        Debug.Log(gameObject.name + " resumed.");
    }

    IEnumerator RewindCooldown()
    {
        canRewind = false;
        yield return new WaitForSeconds(rewindCooldown);
        canRewind = true;
    }
}
