using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperySurface : MonoBehaviour
{
    public float slipperyFactor = 0.5f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Movement playerMovement = collision.GetComponent<Movement>();
            if (playerMovement != null)
            {
                playerMovement.SetSlippery(true, slipperyFactor);
                Debug.Log("Player entered slippery platform.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Movement playerMovement = collision.GetComponent<Movement>();
            if (playerMovement != null)
            {
                playerMovement.SetSlippery(false, 1f);
                Debug.Log("Player left slippery platform.");
            }
        }
    }
}