using UnityEngine;

public class BridgeRockStopTrigger : MonoBehaviour
{
    public BridgeRockSpawner rockSpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (rockSpawner != null)
            {
                rockSpawner.StopSpawning();
            }

            Destroy(gameObject);
        }
    }
}
