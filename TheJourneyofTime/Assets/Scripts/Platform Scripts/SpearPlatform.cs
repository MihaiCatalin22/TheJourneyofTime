using System.Collections;
using UnityEngine;

public class SpearPlatform : MonoBehaviour
{
    public float spearInterval = 3f;
    public GameObject spear;
    public float spearMoveDistance = 1f;
    public float spearSpeed = 2f;

    private Vector3 spearStartPosition;
    private SpriteRenderer[] spearRenderers;
    private Collider2D[] spearColliders;

    private void Start()
    {
        if (spear == null)
        {
            Debug.LogError("Spear GameObject is not assigned.");
            return;
        }

        spearStartPosition = spear.transform.localPosition;

        spearRenderers = spear.GetComponentsInChildren<SpriteRenderer>();
        spearColliders = spear.GetComponentsInChildren<Collider2D>();

        StartCoroutine(SpearAttackRoutine());
    }

    private IEnumerator SpearAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spearInterval);

            SetSpearState(true);

            float startY = spearStartPosition.y;
            float endY = startY + spearMoveDistance;

            while (spear.transform.localPosition.y < endY)
            {
                spear.transform.localPosition += Vector3.up * spearSpeed * Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            SetSpearState(false);
            while (spear.transform.localPosition.y > startY)
            {
                spear.transform.localPosition -= Vector3.up * spearSpeed * Time.deltaTime;
                yield return null;
            }

            
        }
    }

    private void SetSpearState(bool isVisible)
    {
        foreach (var renderer in spearRenderers)
        {
            renderer.enabled = isVisible; 
        }

        foreach (var collider in spearColliders)
        {
            collider.enabled = isVisible; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Movement player = collision.GetComponent<Movement>();
            if (player != null)
            {
                player.SetDead(true);
            }
        }
    }
}
