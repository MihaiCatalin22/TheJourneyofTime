using UnityEngine;

public class ChasingWall : MonoBehaviour
{
    public Transform player; 
    public float speed = 2.0f; // Speed of the wall
    public float followDistance = 5.0f; // Distance behind the player where the wall should stay

    private bool canMove = false; // Controls whether the wall should move
    private Renderer wallRenderer;

    void Awake()
    {
        wallRenderer = GetComponent<Renderer>();
        SetVisibility(false); // Make sure it's invisible initially
    }

    void Update()
    {
        if (canMove)
        {
            Vector3 targetPosition = new Vector3(player.position.x - followDistance, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    public void StartWallMovement()
    {
        canMove = true;
    }

    public void StopWallMovement()
    {
        canMove = false;
    }

    // This method is optional and allows controlling visibility separately
    public void SetVisibility(bool isVisible)
    {
        if (wallRenderer != null)
        {
            wallRenderer.enabled = isVisible;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            // Trigger Die
            Movement playerMovement = other.GetComponent<Movement>();
            if (playerMovement != null)
            {
                Debug.LogWarning("trigger die");
                playerMovement.Die(); 
            }
            else
            {
                Debug.LogWarning("Movement script missing on player object!");
            }
        }
        else if (other.CompareTag("Platform"))
        {
            // Trigger fade 
            PlatformFade platformFade = other.GetComponent<PlatformFade>();
            if (platformFade != null)
            {
                Debug.LogWarning("start fade");
                platformFade.StartFade(); 
            }
            else
            {
                Debug.LogWarning("PlatformFade script missing on platform object!");
            }
        }
    }
}
