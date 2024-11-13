using UnityEngine;

public class WallActivationTrigger : MonoBehaviour
{
    public WallController wallController;
    public bool activateOnEnter = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (activateOnEnter && wallController != null)
            {
                wallController.ActivateWall();
            }
            else if (!activateOnEnter && wallController != null)
            {
                wallController.DeactivateWall();
            }
        }
    }
}
