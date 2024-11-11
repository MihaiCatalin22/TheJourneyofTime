using System.Collections;
using UnityEngine;

public class SpikeProximityTrigger : MonoBehaviour
{
    public ProximityTriggeredFall spikeScript;
    public float fallDelay = 0.2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && spikeScript != null)
        {
            StartCoroutine(TriggerSpikeFall());
        }
    }

    private IEnumerator TriggerSpikeFall()
    {
        yield return new WaitForSeconds(fallDelay);
        
        spikeScript.StartFalling();
    }
}
