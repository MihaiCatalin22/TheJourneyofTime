using System.Collections;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    public float collapseDelay = 1.5f;
    public float minCollapseSpeed = 3f;
    public float maxCollapseSpeed = 9f;
    public float despawnYThreshold = -10f;
    public float colliderDisableDelay = 2f;
    private bool isCollapsing = false;
    private bool isDespawned = false;
    private float collapseSpeed;
    private Collider2D platformCollider;
    private TimeObject timeObject;
    private Renderer platformRenderer;

    // Reference to CollapsingPlatformSound
    private CollapsingPlatformSound collapseSound;

    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        platformRenderer = GetComponent<Renderer>();
        collapseSpeed = Random.Range(minCollapseSpeed, maxCollapseSpeed);

        // Get reference to the sound script
        collapseSound = GetComponent<CollapsingPlatformSound>();
        if (collapseSound == null)
        {
            Debug.LogError("CollapsingPlatformSound component is missing from this GameObject.");
        }

        timeObject = GetComponent<TimeObject>();
        if (timeObject == null)
        {
            Debug.LogError("TimeObject component is missing from this GameObject.");
        }
    }

    private void Update()
    {
        if (timeObject == null || timeObject.isPaused || timeObject.isRewinding) return;

        if (isCollapsing)
        {
            Collapse();
        }

        if (transform.position.y <= despawnYThreshold && !isDespawned)
        {
            Despawn();
        }
    }

    private void Collapse()
    {
        transform.position += Vector3.down * collapseSpeed * Time.deltaTime;
    }

    private void Despawn()
    {
        platformCollider.enabled = false;
        platformRenderer.enabled = false;
        isDespawned = true;
    }

    private void Respawn()
    {
        platformCollider.enabled = true;
        platformRenderer.enabled = true;
        isDespawned = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isCollapsing)
        {
            StartCoroutine(StartCollapseAfterDelay());
        }
    }

    private IEnumerator StartCollapseAfterDelay()
    {
        yield return new WaitForSeconds(collapseDelay);
        isCollapsing = true;

        // Only play the sound when collapse starts
        if (collapseSound != null && !collapseSound.collapseAudioSource.isPlaying)
        {
            collapseSound.PlayCollapseSound();
            Debug.Log("Playing Collapsing Platform Sound"); // Debugging
        }

        yield return new WaitForSeconds(colliderDisableDelay);
        platformCollider.enabled = false;
    }
}
