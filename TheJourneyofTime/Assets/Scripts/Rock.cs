using System.Collections;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float despawnYThreshold = -10f;
    private Vector3 startPosition;
    private bool isFalling = false;
    private Collider2D rockCollider;
    private SpriteRenderer rockRenderer;
    private Rigidbody2D rb;

    private void Start()
    {
        startPosition = transform.position;
        rockCollider = GetComponent<Collider2D>();
        rockRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        rockRenderer.enabled = false;
        rockCollider.enabled = false;
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (isFalling)
        {
            Fall();

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
        rockCollider.enabled = true;
        rockRenderer.enabled = true;
    }

    private void Fall()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    public void DeactivateRock()
    {
        isFalling = false;
        rockCollider.enabled = false;
        rockRenderer.enabled = false;
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        DeactivateRock();
    }
}
