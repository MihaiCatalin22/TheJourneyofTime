using UnityEngine;

public class RockImpact : MonoBehaviour
{
    private RockImpactSound impactSoundScript;

    private void Start()
    {
        impactSoundScript = GetComponent<RockImpactSound>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BridgeSegment segment = other.GetComponent<BridgeSegment>();
        Rock rockScript = GetComponent<Rock>();

        if (segment != null)
        {
            segment.TakeHit();

            if (impactSoundScript != null)
            {
                impactSoundScript.PlayImpactSound();
            }

            if (rockScript != null)
            {
                rockScript.ResetPosition();
            }
        }
        else
        {
            if (impactSoundScript != null)
            {
                impactSoundScript.PlayImpactSound();
            }

            if (rockScript != null)
            {
                rockScript.ResetPosition();
            }
        }
    }
}
