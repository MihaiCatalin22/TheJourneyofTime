using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Dash")]
    public Image dashIcon;
    public Color availableColor = Color.white;
    public Color unavailableColor = Color.gray;

    [Header("Time Manipulation")]
    public Slider timeStopTimer;
    public Slider timeRewindTimer;

    private Movement playerMovement;
    private ManagerTime timeManager;

    private void Start()
    {
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
