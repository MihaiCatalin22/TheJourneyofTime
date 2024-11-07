using UnityEngine;

public class BridgeRockStopTrigger : MonoBehaviour
{
    public BridgeRockSpawner rockSpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the BridgeRockStopTrigger.");

            // Stop spawning rocks and deactivate all rocks in the spawner
            if (rockSpawner != null)
            {
                rockSpawner.StopSpawning();
            }

            // Destroy this trigger after activation to prevent re-triggering
            Destroy(gameObject);
        }
    }
}
