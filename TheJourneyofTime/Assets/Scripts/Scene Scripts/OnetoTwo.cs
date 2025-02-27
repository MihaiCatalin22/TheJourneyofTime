using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnetoTwo : MonoBehaviour
{
    public string nextSceneName = "End - Level 2";
    public TransitionSound transitionSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.Instance.ClearCheckpoint(); 
            
            if (transitionSound != null && transitionSound.transitionClip != null)
            {
                transitionSound.PlayTransitionSound();
                StartCoroutine(WaitAndLoadScene(transitionSound.transitionClip.length));
            }
            else
            {
                LoadNextScene();
            }
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator WaitAndLoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadNextScene();
    }
}
