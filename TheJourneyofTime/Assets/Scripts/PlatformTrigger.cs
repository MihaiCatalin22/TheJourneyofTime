using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public Animator platformAnimator;
    private bool hasPlayed = false; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with platform!");
            platformAnimator.SetTrigger("Crack");
            Debug.Log("Crack trigger set in Animator");
            hasPlayed = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (platformAnimator == null)
        {
            platformAnimator = GetComponent<Animator>();
            if (platformAnimator == null)
            {
                Debug.LogError("No Animator component found on platform!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
