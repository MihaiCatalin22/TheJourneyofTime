using UnityEngine;

public class BridgeRockStartTrigger : MonoBehaviour
{
    public BridgeRockSpawner rockSpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the BridgeRockStartTrigger.");

            // Start spawning rocks when the player enters the trigger area
            if (rockSpawner != null)
            {
                rockSpawner.StartSpawning();
            }

            // Destroy this trigger after activation to prevent re-triggering
            Destroy(gameObject);
        }
    }
}
