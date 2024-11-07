using System.Collections;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float despawnYThreshold = -10f; // Y position at which the rock should reset
    private Vector3 startPosition;
    private bool isFalling = false;
    private Collider2D rockCollider;
    private SpriteRenderer rockRenderer;
    private Rigidbody2D rb;

    private void Start()
    {
        startPosition = transform.position;  // Save the initial position to reset to
        rockCollider = GetComponent<Collider2D>();
        rockRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Initial setup: Make the rock invisible and disable physics
        rockRenderer.enabled = false;
        rockCollider.enabled = false;
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (isFalling)
        {
            Fall();

            // Check if rock has fallen below the threshold
            if (transform.position.y <= despawnYThreshold)
            {
                ResetPosition();
            }
        }
    }

    public void StartFalling()
    {
        if (rockCollider == null || rockRenderer == null || rb == null)
        {
            Debug.LogError($"{gameObject.name} cannot start falling because a required component is missing!");
            return;
        }

        isFalling = true;
        rockCollider.enabled = true;      // Enable collider while falling
        rockRenderer.enabled = true;      // Make the rock visible
    }

    private void Fall()
    {
        // Manually move the rock downwards
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    public void DeactivateRock()
    {
        // Stop falling and hide the rock
        isFalling = false;
        rockCollider.enabled = false;
        rockRenderer.enabled = false;
    }

    public void ResetPosition()
    {
        // Reset position to the start and deactivate the rock
        transform.position = startPosition;
        DeactivateRock();
    }
}
