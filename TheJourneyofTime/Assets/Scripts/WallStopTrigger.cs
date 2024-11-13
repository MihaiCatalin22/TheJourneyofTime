using UnityEngine;

public class WallStopTrigger : MonoBehaviour
{
    public ChasingWall chasingWall;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (chasingWall != null)
            {
                chasingWall.StopWallMovement();
                Debug.Log("Wall movement stopped.");
            }
            else
            {
                Debug.LogWarning("ChasingWall reference is missing in WallStopTrigger.");
            }
        }
    }
}
