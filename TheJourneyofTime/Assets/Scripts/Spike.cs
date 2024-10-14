using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this to enable scene reloading

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
                
                // Start the death effect and then reload the scene
                StartCoroutine(ChangeColorAndReloadScene(playerMovement));
            }
        }
    }

    private IEnumerator ChangeColorAndReloadScene(Movement playerMovement)
    {
        // Change color to red to indicate death
        SpriteRenderer spriteRenderer = playerMovement.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;

        // Wait for a short duration before reloading the scene
        yield return new WaitForSeconds(1f);

        // Reload the current scene to restart everything
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
