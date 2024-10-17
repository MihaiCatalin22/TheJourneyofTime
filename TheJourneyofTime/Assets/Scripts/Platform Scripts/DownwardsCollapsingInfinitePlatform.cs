using System.Collections;
using UnityEngine;

public class DownwardsCollapsingInfinitePlatform : TimeObject
{
    public float moveSpeed = 2f;
    public float groundYPosition = -9f;
    public float respawnYPosition = 20f;
    public float collapseSpeed = 5f;
    public float dropThroughDuration = 0.5f;
    private bool isCollapsing = false;
    private bool isRespawning = false;
    private Vector3 initialLocalPosition;
    private Collider2D platformCollider;
    private Coroutine collapseCoroutine;

    private void Start()
    {
        initialLocalPosition = transform.localPosition;
        platformCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!isRespawning && !isPaused)
        {
            if (!isCollapsing)
            {
                MoveDownwards();
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

    private void MoveDownwards()
    {
        transform.localPosition += Vector3.down * moveSpeed * Time.deltaTime;
    }

    private void CollapseDownwards()
    {
        transform.localPosition += Vector3.down * collapseSpeed * Time.deltaTime;
    }

    private void CheckPositionAndRespawn()
    {
        if (transform.localPosition.y <= groundYPosition && !isRespawning)
        {
            StartCoroutine(RespawnAtTopWithDelay(Random.Range(0.5f, 1.5f)));
        }
    }

    private IEnumerator RespawnAtTopWithDelay(float delay)
    {
        isRespawning = true;
        yield return new WaitForSeconds(delay);
        transform.localPosition = new Vector3(initialLocalPosition.x, respawnYPosition, initialLocalPosition.z);
        isRespawning = false;
        isCollapsing = false;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Player"));
        return hit.collider != null;
    }
}
