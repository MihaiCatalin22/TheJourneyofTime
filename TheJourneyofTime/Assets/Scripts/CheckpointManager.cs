using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private Vector3 currentCheckpoint = Vector3.zero;
    private GameObject player;
    private string currentSceneName;

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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentSceneName = SceneManager.GetActiveScene().name; // Store the current scene name

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Callback when the scene finishes loading
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // If the scene changes (not reloaded), clear the checkpoint
        if (scene.name != currentSceneName)
        {
            ClearCheckpoint();
            currentSceneName = scene.name;
        }

        // If a checkpoint exists, move the player to the checkpoint position
        if (HasCheckpoint() && player != null)
        {
            player.transform.position = currentCheckpoint;
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

    // Clear the checkpoint, used when transitioning to a new scene
    public void ClearCheckpoint()
    {
        currentCheckpoint = Vector3.zero;
        Debug.Log("Checkpoint cleared.");
    }

    public void RespawnPlayer()
    {
        // Reload the scene, which will trigger the respawn
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
