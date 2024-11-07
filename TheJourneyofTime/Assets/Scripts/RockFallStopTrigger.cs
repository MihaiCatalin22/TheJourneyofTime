using UnityEngine;

public class RockFallStopTrigger : MonoBehaviour
{
    public RockSpawner rockSpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the RockFallStopTrigger area.");

            if (rockSpawner != null)
            {
                // Call DeactivateAllRocks to stop spawning and hide all rocks
                rockSpawner.DeactivateAllRocks();
            }

            // Destroy this trigger to prevent reactivation
            Destroy(gameObject);
        }
    }
}
