using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public List<GameObject> rockLines;  // List of rocks in the order they should fall
    public float fallInterval = 0.75f;   // Time interval between each rock starting to fall
    public float respawnDelay = 2.0f;
    private Coroutine spawnCoroutine;
    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(StartRockFallSequence());
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    public void DeactivateAllRocks()
    {
        // Stop spawning rocks
        StopSpawning();

        // Deactivate each rock in the list
        foreach (GameObject rock in rockLines)
        {
            Rock rockScript = rock.GetComponent<Rock>();
            if (rockScript != null)
            {
                rockScript.DeactivateRock();
            }
        }
    }
    
    private IEnumerator StartRockFallSequence()
    {
        while (true)
        {
            foreach (GameObject rock in rockLines)
            {
                Rock rockScript = rock.GetComponent<Rock>();
                if (rockScript != null)
                {
                    rockScript.StartFalling();
                }
                yield return new WaitForSeconds(fallInterval);
            }
            yield return new WaitForSeconds(respawnDelay);
        }
    }
}