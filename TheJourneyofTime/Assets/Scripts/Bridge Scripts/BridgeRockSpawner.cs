using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeRockSpawner : MonoBehaviour
{
    public List<GameObject> rockLines;    
    public float fallInterval = 0.75f;    
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
        foreach (GameObject rock in rockLines)
        {
            if (rock != null)
            {
                Rock rockScript = rock.GetComponent<Rock>();
                if (rockScript != null)
                {
                    rockScript.DeactivateRock();
                }
            }
        }
    }

    private IEnumerator StartRockFallSequence()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, rockLines.Count);
            GameObject randomRock = rockLines[randomIndex];

            if (randomRock != null)
            {
                Rock rockScript = randomRock.GetComponent<Rock>();
                if (rockScript != null)
                {
                    rockScript.StartFalling();
                }
            }
            yield return new WaitForSeconds(fallInterval);
        }
    }
}
