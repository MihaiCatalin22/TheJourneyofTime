using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreetoTutorial : MonoBehaviour
{
    public string nextSceneName = "TutorialScene";

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
