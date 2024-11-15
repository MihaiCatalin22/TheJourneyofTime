using UnityEngine;
using System.Collections;

public class FastCollapsingPlatform : MonoBehaviour
{
    public float minCollapseSpeed = 3f;
    public float maxCollapseSpeed = 7f;
    public float detectionRadius = 10f;
    public float despawnYThreshold = -10f;
    public float colliderDisableDelay = 2f;

    private bool isCollapsing = false;
    private bool isDespawned = false;
    private float collapseSpeed;
    private Collider2D platformCollider;
    private TimeObject timeObject;
    private Renderer platformRenderer;

    private FastCollapsingPlatformSound fastCollapseSound;

    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        platformRenderer = GetComponent<Renderer>();
        collapseSpeed = Random.Range(minCollapseSpeed, maxCollapseSpeed);

        fastCollapseSound = GetComponent<FastCollapsingPlatformSound>();
        if (fastCollapseSound == null)
        {
            Debug.LogError("FastCollapsingPlatformSound component is missing from this GameObject.");
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollapsing)
        {
            StartCoroutine(StartCollapse());
        }
    }

    private IEnumerator StartCollapse()
    {
        isCollapsing = true;

        if (fastCollapseSound != null && !fastCollapseSound.fastCollapseAudioSource.isPlaying)
        {
            fastCollapseSound.PlayFastCollapsingSound();
            Debug.Log("Playing Fast Collapsing Platform Sound"); // Debugging
        }

        yield return new WaitForSeconds(colliderDisableDelay);
        platformCollider.enabled = false;
    }
}
