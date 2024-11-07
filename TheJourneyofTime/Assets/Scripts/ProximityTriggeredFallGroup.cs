using System.Collections;
using UnityEngine;

public class ProximityTriggeredFallGroup : MonoBehaviour
{
    public float fallDelay = 0.2f;
    private bool isFalling = false;
    private Rigidbody2D[] spikeRigidbodies;
    private TimeObject[] timeObjects;

    public FallingSpikeSound fallingSpikeSound; // Sound reference

    private void Start()
    {
        spikeRigidbodies = GetComponentsInChildren<Rigidbody2D>();
        timeObjects = GetComponentsInChildren<TimeObject>();

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
            rb.isKinematic = false;
            rb.gravityScale = 1;
        }
        Debug.Log("Spike group falling!");

        if (fallingSpikeSound != null)
        {
            fallingSpikeSound.PlayFallingSound();
        }
    }
}
