using UnityEngine;

public class RockFallStopTrigger : MonoBehaviour
{
    public RockSpawner rockSpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (rockSpawner != null)
            {
                rockSpawner.DeactivateAllRocks();
            }

            Destroy(gameObject);
        }
    }
}
