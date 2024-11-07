using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreetoTutorial : MonoBehaviour
{
    public string nextSceneName = "TutorialScene";

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
