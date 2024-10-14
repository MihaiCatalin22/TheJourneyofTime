using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.CompareTag("Player") && gameObject.CompareTag("Deadly"))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            
            if (playerRb != null)
            {
                // moves players x
                playerRb.transform.position = new Vector3(playerRb.transform.position.x, playerRb.transform.position.y, -10);
            
                // disables player's collider
                collision.GetComponent<Collider2D>().enabled = false;

                // color change
                StartCoroutine(ChangeColorTemporarily(collision.gameObject));

            }
            
        }
    }
    private IEnumerator ChangeColorTemporarily(GameObject player)
    {
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // og color
            Color originalColor = spriteRenderer.color;

            // change to red
            spriteRenderer.color = Color.red;

            // wait a sec
            yield return new WaitForSeconds(0.2f);

            // back to og color
            spriteRenderer.color = originalColor;
        }
    }
}
