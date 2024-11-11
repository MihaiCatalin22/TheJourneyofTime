using System.Collections;
using UnityEngine;

public class ProximityTriggeredFallGroup : MonoBehaviour
{
    public float fallDelay = 0.2f;
    private bool isFalling = false;
    private Rigidbody2D[] spikeRigidbodies;
    private TimeObject[] timeObjects;

    public FallingSpikeSound fallingSpikeSound;

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

        if (fallingSpikeSound != null)
        {
            fallingSpikeSound.PlayFallingSound();
        }
    }
}
