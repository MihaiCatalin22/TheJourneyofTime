using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialToStart : MonoBehaviour
{
    public string nextSceneName = "End - Level 1";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.Instance.ClearCheckpoint(); 
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
