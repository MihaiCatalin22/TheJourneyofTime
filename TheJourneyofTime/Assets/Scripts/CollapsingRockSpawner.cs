using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingRockSpawner : MonoBehaviour
{
    public GameObject[] rockPrefabs;
    public float spawnInterval = 2f;
    public Vector2 spawnOffset = new Vector2(1.5f, 1.5f);
    public float initialHorizontalForce = 2f;
    public Transform player;

    private float spawnTimer = 0f;
   
   void Update()
    {
        // update spawn timer
        spawnTimer += Time.deltaTime;

        // check if its time to spawn a new rock
        if (spawnTimer >= spawnInterval)
        {
            SpawnRock();
            spawnTimer = 0f; // reset timer
        }
    }

    void SpawnRock()
    {
        GameObject rockPrefab = rockPrefabs[Random.Range(0, rockPrefabs.Length)];

        // Calculate a precise spawn position in front and at the top right of the wall
        Vector3 spawnPosition = transform.position + new Vector3(spawnOffset.x, spawnOffset.y, 0f);
        
        GameObject rockInstance = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = rockInstance.GetComponent<Rigidbody2D>();
        if (rb != null && player != null)
        {
            float direction = Mathf.Sign(player.position.x - spawnPosition.x);
            rb.AddForce(new Vector2(direction * initialHorizontalForce, 0), ForceMode2D.Impulse);
        }
    }
}
