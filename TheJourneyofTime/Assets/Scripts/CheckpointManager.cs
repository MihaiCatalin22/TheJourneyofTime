using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private Vector3 currentCheckpoint = Vector3.zero; // Initialize to zero as default
    private GameObject player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        currentCheckpoint = checkpointPosition;
        Debug.Log("Checkpoint set at: " + checkpointPosition);
    }

    public bool HasCheckpoint()
    {
        return currentCheckpoint != Vector3.zero; // Returns true if a checkpoint has been set
    }

    public void RespawnPlayer(GameObject player)
    {
        if (HasCheckpoint())
        {
            player.transform.position = currentCheckpoint;
            Debug.Log("Player respawned at checkpoint.");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload if no checkpoint
            Debug.Log("No checkpoint set. Reloading scene.");
        }
    }
}
