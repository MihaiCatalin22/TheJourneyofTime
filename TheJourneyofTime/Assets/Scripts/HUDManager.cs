using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Dash")]
    public Image dashIcon; // Icon for dash status
    public Color availableColor = Color.white; // Color when dash is available
    public Color unavailableColor = Color.gray; // Color when dash is unavailable

    [Header("Time Manipulation")]
    public Slider timeStopTimer; // Circular clock for time stop
    public Slider timeRewindTimer; // Circular clock for time rewind

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
    if (playerMovement != null && dashIcon != null)
    {
        dashIcon.color = playerMovement.canDash ? availableColor : unavailableColor;
    }
}

    private void UpdateTimeStopTimer()
    {
        timeStopTimer.value = timeManager.GetTimeStopFillAmount();
    }


    private void UpdateTimeRewindTimer()
    {
        timeRewindTimer.value = timeManager.GetRewindFillAmount();
    }

}
