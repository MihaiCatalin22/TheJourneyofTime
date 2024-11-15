using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFade : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    private bool isFading = false; 
    private SpriteRenderer spriteRenderer;
    private Collider2D[] colliders;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliders = GetComponents<Collider2D>();
    }

    public void StartFade()
    {
        if (!isFading)
        {
            DisableColliders();
            StartCoroutine(FadeOut());
        }
    }

    private void DisableColliders()
    {
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }
    }

    private IEnumerator FadeOut()
    {
        isFading = true;
        float elapsedTime = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
