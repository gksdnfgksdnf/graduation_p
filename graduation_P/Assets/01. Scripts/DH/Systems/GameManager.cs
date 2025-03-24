using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DayManager.Instance.StartDay();
        DayManager.Instance.onPhase += HandlePhaseChange;
        DayManager.Instance.onHour += HandleHourChange;
    }

    private void HandlePhaseChange(DayPhase phase)
    {
        CustomerManager.Instance.PhaseUpdate(DayManager.Instance.day, phase);
    }

    private void HandleHourChange(int hour)
    {
        CustomerManager.Instance.HourUpdate(DayManager.Instance.phase, hour);
    }
}
