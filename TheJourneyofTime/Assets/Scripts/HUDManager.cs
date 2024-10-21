using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Dash")]
    public Image dashIcon; // Icon for dash status
    public Color availableColor = Color.white; // Color when dash is available
    public Color unavailableColor = Color.gray; // Color when dash is unavailable

    [Header("Time Manipulation")]
    public Image timeStopTimer; // Circular clock for time stop
    public Image timeRewindTimer; // Circular clock for time rewind

    private Movement playerMovement;
    private ManagerTime timeManager;

    private void Start()
    {
        // Find references to the player's movement and time management scripts
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        timeManager = FindObjectOfType<ManagerTime>();

        if (playerMovement == null || timeManager == null)
        {
            Debug.LogError("PlayerMovement or ManagerTime script not found!");
        }
    }

    private void Update()
    {
        UpdateDashIcon();
        UpdateTimeStopTimer();
        UpdateTimeRewindTimer();
    }

    private void UpdateDashIcon()
    {
        if (playerMovement.canDash)
        {
            dashIcon.color = availableColor; // Set color to show availability
        }
        else
        {
            dashIcon.color = unavailableColor; // Grayed out when unavailable
        }
    }

    private void UpdateTimeStopTimer()
    {
        if (timeManager.isTimeStopped)
        {
            timeStopTimer.fillAmount = timeManager.stopDuration / timeManager.stopCooldownDuration;
        }
        else if (timeManager.isStopCooldownActive)
        {
            timeStopTimer.fillAmount = (timeManager.stopCooldownDuration - Mathf.Max(0, timeManager.stopCooldownDuration)) / timeManager.stopCooldownDuration;
        }
        else
        {
            timeStopTimer.fillAmount = 1f; // Timer full when ready
        }
    }

    private void UpdateTimeRewindTimer()
    {
        if (timeManager.isRewinding)
        {
            timeRewindTimer.fillAmount = timeManager.rewindDuration / timeManager.rewindCooldownDuration;
        }
        else if (timeManager.isRewindCooldownActive)
        {
            timeRewindTimer.fillAmount = (timeManager.rewindCooldownDuration - Mathf.Max(0, timeManager.rewindCooldownDuration)) / timeManager.rewindCooldownDuration;
        }
        else
        {
            timeRewindTimer.fillAmount = 1f; // Timer full when ready
        }
    }
}
