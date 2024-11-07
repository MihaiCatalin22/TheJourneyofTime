using System.Collections;
using UnityEngine;

public class ProximityTriggeredFall : MonoBehaviour
{
    public float fallDelay = 0.2f;
    private bool isFalling = false;
    private Rigidbody2D rb;
    private TimeObject timeObject;

    public FallingSpikeSound fallingSpikeSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D found on " + gameObject.name);
        }
        rb.isKinematic = true;

        timeObject = GetComponent<TimeObject>();
        if (timeObject == null)
        {
            Debug.LogError("TimeObject component is missing from " + gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFalling)
        {
            Debug.Log("Player detected. Starting fall delay.");
            StartCoroutine(FallAfterDelay(fallDelay));
        }
    }

    private IEnumerator FallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!isFalling && !timeObject.isPaused && !timeObject.isRewinding)
        {
            StartFalling();
        }
    }

    public void StartFalling()
    {
        if (isFalling) return;
        isFalling = true;
        rb.isKinematic = false;
        rb.gravityScale = 1;
        Debug.Log("Trap falling!");

        if (fallingSpikeSound != null)
        {
            fallingSpikeSound.PlayFallingSound();
        }
    }
}
