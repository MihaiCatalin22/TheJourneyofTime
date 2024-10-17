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

                StartCoroutine(ChangeColorAndRespawn(playerMovement, collision.gameObject));
            }
        }
    }

    private IEnumerator ChangeColorAndRespawn(Movement playerMovement, GameObject player)
    {
        SpriteRenderer spriteRenderer = playerMovement.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(1f);

        CheckpointManager.Instance.RespawnPlayer(player);

        playerMovement.ResetMovementState();

        spriteRenderer.color = originalColor;
        player.GetComponent<Collider2D>().enabled = true;
    }
}
