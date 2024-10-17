using System.Collections;
using UnityEngine;

public class ProximityTriggeredFallGroup : MonoBehaviour
{
    public float fallDelay = 0.2f; // Delay before all spikes fall after player detection
    private bool isFalling = false;
    private Rigidbody2D[] spikeRigidbodies; // Array for all spike Rigidbodies in the group
    private TimeObject[] timeObjects; // Array for time control

    private void Start()
    {
        // Get all Rigidbody2D components in children
        spikeRigidbodies = GetComponentsInChildren<Rigidbody2D>();
        timeObjects = GetComponentsInChildren<TimeObject>();

        // Set all Rigidbody2D to be kinematic initially to prevent falling
        foreach (var rb in spikeRigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFalling)
        {
            Debug.Log("Player detected near spike group. Starting fall delay.");
            StartCoroutine(FallAfterDelay(fallDelay));
        }
    }

    private IEnumerator FallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!isFalling)
        {
            StartFalling();
        }
    }

    private void StartFalling()
    {
        isFalling = true;

        foreach (var rb in spikeRigidbodies)
        {
            rb.isKinematic = false; // Enable physics for falling
            rb.gravityScale = 1; // Adjust gravity as needed for fall speed
        }
        Debug.Log("Spike group falling!");
    }

    private void Update()
    {
        foreach (var timeObject in timeObjects)
        {
            if (timeObject.isPaused || timeObject.isRewinding)
            {
                return; // Prevent falling while paused or rewinding
            }
        }
    }
}
