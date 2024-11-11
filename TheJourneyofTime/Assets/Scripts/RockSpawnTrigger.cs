using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawnTrigger : MonoBehaviour
{
    public RockSpawner rockSpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (rockSpawner != null)
            {
                rockSpawner.StartSpawning();
            }

            Destroy(gameObject);
        }
    }
}