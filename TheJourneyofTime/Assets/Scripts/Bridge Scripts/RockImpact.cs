using UnityEngine;

public class RockImpact : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        BridgeSegment segment = other.GetComponent<BridgeSegment>();
        Rock rockScript = GetComponent<Rock>();

        if (segment != null)
        {
            segment.TakeHit();

            if (rockScript != null)
            {
                rockScript.ResetPosition();  // Reset the rock instead of destroying it
            }
        }
        else
        {
            if (rockScript != null)
            {
                rockScript.ResetPosition();  // Reset even if it collides with other objects
            }
        }
    }
}
