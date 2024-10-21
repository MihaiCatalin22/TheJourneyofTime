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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentSceneName = SceneManager.GetActiveScene().name;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (scene.name != currentSceneName)
        {
            ClearCheckpoint();
            currentSceneName = scene.name;
        }

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
        return currentCheckpoint != Vector3.zero;
    }

    public void ClearCheckpoint()
    {
        currentCheckpoint = Vector3.zero;
        Debug.Log("Checkpoint cleared.");
    }

    public void RespawnPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
