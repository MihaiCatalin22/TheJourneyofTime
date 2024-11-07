using UnityEngine;
using System.Collections;

public class BridgeSegment : MonoBehaviour
{
    public int maxHitsBeforeCollapse = 3;
    public float collapseDelay = 0.5f;
    private int hitCount = 0;
    private bool isCollapsing = false;

    private Collider2D segmentCollider;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        segmentCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (segmentCollider == null || spriteRenderer == null)
        {
            Debug.LogError($"{gameObject.name} is missing required components!");
        }
    }


    public void TakeHit()
    {
        if (isCollapsing) return;

        hitCount++;
        Debug.Log($"{gameObject.name} hit {hitCount} times");

        if (hitCount >= maxHitsBeforeCollapse)
        {
            TriggerCollapse();
        }
        else
        {
            StartCoroutine(FlashWarning());
        }
    }

    private void TriggerCollapse()
    {
        isCollapsing = true;
        StartCoroutine(Collapse());
    }

    private IEnumerator Collapse()
    {
        yield return new WaitForSeconds(collapseDelay);

        segmentCollider.enabled = false;
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }

    private IEnumerator FlashWarning()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }
}
