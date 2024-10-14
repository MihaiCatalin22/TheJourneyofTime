using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("Deadly"))
        {
            Movement playerMovement = collision.GetComponent<Movement>();

            if (playerMovement != null)
            {
                playerMovement.SetDead(true); // Disable player movement
                collision.GetComponent<Collider2D>().enabled = false;
                StartCoroutine(ChangeColorAndRespawn(playerMovement, collision));
            }
        }
    }

    private IEnumerator ChangeColorAndRespawn(Movement playerMovement, Collider2D playerCollider)
    {
        SpriteRenderer spriteRenderer = playerMovement.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;

        Rigidbody2D rb = playerMovement.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, -playerMovement.jumpForce);

        yield return new WaitForSeconds(0.68f);

        spriteRenderer.color = originalColor;
        playerCollider.enabled = true;

        yield return playerMovement.StartCoroutine(playerMovement.Respawn());
        playerMovement.SetDead(false); // Re-enable player movement after respawn
    }
}
