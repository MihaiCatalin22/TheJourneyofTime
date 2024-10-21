using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Movement playerMovement = collision.GetComponent<Movement>();

            if (playerMovement != null)
            {
                playerMovement.SetDead(true);
                collision.GetComponent<Collider2D>().enabled = false;

                StartCoroutine(ChangeColorAndRespawn(playerMovement));
            }
        }
    }

    private IEnumerator ChangeColorAndRespawn(Movement playerMovement)
    {
        SpriteRenderer spriteRenderer = playerMovement.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.4f);

        CheckpointManager.Instance.RespawnPlayer();

        playerMovement.ResetMovementState();

        spriteRenderer.color = originalColor;
    }
}
