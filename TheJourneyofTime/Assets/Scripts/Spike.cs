using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

                StartCoroutine(ChangeColorAndReloadScene(playerMovement));
            }
        }
    }

    private IEnumerator ChangeColorAndReloadScene(Movement playerMovement)
    {
        SpriteRenderer spriteRenderer = playerMovement.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
