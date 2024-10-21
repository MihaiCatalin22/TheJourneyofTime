using System.Collections;
using UnityEngine;

public class UpwardsCollapsingInfinitePlatform : TimeObject
{
    public float moveSpeed = 2f;
    public float ceilingYPosition = 20f; 
    public float respawnYPosition = -9f; 
    public float collapseSpeed = 5f;
    public float dropThroughDuration = 0.5f;

    private bool isCollapsing = false;
    private bool isRespawning = false;
    private Collider2D platformCollider;
    private Coroutine collapseCoroutine;

    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();
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
    }

    private void CollapseDownwards()
    {
        transform.localPosition += Vector3.down * collapseSpeed * Time.deltaTime;
    }

    private void CheckPositionAndRespawn()
    {
        if (transform.localPosition.y >= ceilingYPosition && !isRespawning)
        {
            StartCoroutine(RespawnAtBottomWithDelay(Random.Range(0.5f, 1.5f)));
        }
    }

    private IEnumerator RespawnAtBottomWithDelay(float delay)
    {
        isRespawning = true;
        yield return new WaitForSeconds(delay);

        transform.localPosition = new Vector3(transform.localPosition.x, respawnYPosition, transform.localPosition.z);
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
        yield return new WaitForSeconds(Random.Range(1f, 1.5f));
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
