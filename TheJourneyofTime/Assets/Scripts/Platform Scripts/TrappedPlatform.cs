using System.Collections;
using UnityEngine;

public class TrappedPlatform : MonoBehaviour
{
    public GameObject Spike;
    public float spikesDelay = 0.5f;
    public int flashCount = 10;
    public float flashDuration = 0.05f; 

    private bool isTriggered = false;
    private TimeObject timeObject;
    private Renderer platformRenderer;
    private Color originalColor;
    public Color flashColor = Color.red;

    private void Start()
    {
        timeObject = GetComponent<TimeObject>();
        platformRenderer = GetComponent<Renderer>();

        if (timeObject == null)
        {
            Debug.LogError("TimeObject component is missing from this GameObject.");
        }

        if (platformRenderer != null)
        {
            originalColor = platformRenderer.material.color;
        }
        else
        {
            Debug.LogError("Renderer component is missing from this GameObject.");
        }

        if (Spike != null)
        {
            Spike.SetActive(false);
        }
        else
        {
            Debug.LogError("Spike GameObject is not assigned to the TrappedPlatform.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(ActivateTrap());
        }
    }

    private IEnumerator ActivateTrap()
    {
        if (timeObject != null)
        {
            timeObject.isRewinding = false;
        }

        for (int i = 0; i < flashCount; i++)
        {
            platformRenderer.material.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            platformRenderer.material.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }

        yield return new WaitForSeconds(spikesDelay - flashCount * flashDuration * 2);

        if (Spike != null)
        {
            Spike.SetActive(true);
        }

        // Movement playerMovement = FindObjectOfType<Movement>();
        // if (playerMovement != null)
        // {
        //     playerMovement.SetDead(true);
        // }
    }
}
