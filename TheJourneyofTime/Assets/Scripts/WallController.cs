using UnityEngine;

public class WallController : MonoBehaviour
{
    public ChasingWall chasingWall; // Reference to the ChasingWall script

    void Start()
    {
        if (chasingWall != null)
        {
            chasingWall.StopWallMovement(); // Ensure the wall is inactive at the start
            chasingWall.SetVisibility(false); // Make it invisible initially (optional)
        }
    }

    public void ActivateWall()
    {
        if (chasingWall != null)
        {
            chasingWall.SetVisibility(true); // Make the wall visible
            chasingWall.StartWallMovement(); // Start its movement
            Debug.Log("Wall movement activated.");
        }
    }

    public void DeactivateWall()
    {
        if (chasingWall != null)
        {
            chasingWall.StopWallMovement(); // Stop its movement only
            Debug.Log("Wall movement deactivated.");
        }
    }

    // Optional method to hide the wall entirely
    public void HideWall()
    {
        if (chasingWall != null)
        {
            chasingWall.SetVisibility(false); // Make it invisible
            Debug.Log("Wall hidden.");
        }
    }
}
