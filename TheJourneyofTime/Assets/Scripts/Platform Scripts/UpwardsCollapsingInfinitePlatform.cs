using System.Collections;
using UnityEngine;

public class UpwardsCollapsingInfinitePlatform : TimeObject
{
    public float moveSpeed = 2f;
    public float ceilingYPosition = 20f; // Ensure this goes up to Y = 20
    public float respawnYPosition = -9f; // Respawn at Y = -9 after reaching Y = 20
    public float collapseSpeed = 5f;
    public float dropThroughDuration = 0.5f;

    private bool isCollapsing = false;
    private bool isRespawning = false;
    private Collider2D platformCollider;
    private Coroutine collapseCoroutine;

    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        // Start the platform at its initial position without offset adjustments
    }

    private void Update()
    {
        if (!isRespawning && !isPaused)
        {
            if (!isCollapsing)
            {
                MoveUpwards();
            }
            else
            {
                CollapseDownwards();
            }
        }

        CheckPositionAndRespawn();

        if (Input.GetKeyDown(KeyCode.S) && IsPlayerOnPlatform() && !isRewinding)
        {
            StartCoroutine(DropThrough());
        }
    }

    private void MoveUpwards()
    {
        transform.localPosition += Vector3.up * moveSpeed * Time.deltaTime;
        Debug.Log($"Moving Up: Current Y = {transform.localPosition.y}"); // Keep logging to monitor movement
    }

    private void CollapseDownwards()
    {
        transform.localPosition += Vector3.down * collapseSpeed * Time.deltaTime;
    }

    private void CheckPositionAndRespawn()
    {
        // Only respawn when the platform reaches or exceeds Y = 20
        if (transform.localPosition.y >= ceilingYPosition && !isRespawning)
        {
            Debug.Log("Reached Ceiling - Respawning at Bottom");
            StartCoroutine(RespawnAtBottomWithDelay(Random.Range(0.5f, 1.5f)));
        }
    }

    private IEnumerator RespawnAtBottomWithDelay(float delay)
    {
        isRespawning = true;
        yield return new WaitForSeconds(delay);

        // Respawn the platform at the specified Y = -9 position
        transform.localPosition = new Vector3(transform.localPosition.x, respawnYPosition, transform.localPosition.z);
        isRespawning = false;
        isCollapsing = false;
        Debug.Log($"Respawned at Bottom: Current Y = {transform.localPosition.y}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isCollapsing && !isRewinding)
        {
            collapseCoroutine = StartCoroutine(CollapseAfterDelay());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && collapseCoroutine != null)
        {
            StopCoroutine(collapseCoroutine);
        }
    }

    private IEnumerator CollapseAfterDelay()
    {
        yield return new WaitForSeconds(Random.Range(2.5f, 3.5f));
        isCollapsing = true;
        platformCollider.enabled = false;
    }

    private IEnumerator DropThrough()
    {
        platformCollider.enabled = false;
        yield return new WaitForSeconds(dropThroughDuration);
        platformCollider.enabled = true;
    }

    private bool IsPlayerOnPlatform()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.1f, LayerMask.GetMask("Player"));
        return hit.collider != null;
    }
}
