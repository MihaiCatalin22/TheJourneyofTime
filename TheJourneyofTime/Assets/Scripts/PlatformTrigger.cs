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
            platformAnimator.SetTrigger("Crack");
            hasPlayed = true;
        }
    }

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
    void Update()
    {
        
    }
}
