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
        DayManager.Instance.onPhase.Add(HandlePhaseChange);
        DayManager.Instance.onHour.Add(HandleHourChange);
        DayManager.Instance.StartDay();
    }

    private void HandlePhaseChange(EventWaiter waiter)
    {
        CustomerManager.Instance.PhaseUpdate(waiter);
    }

    private void HandleHourChange(EventWaiter waiter)
    {
        CustomerManager.Instance.HourUpdate(waiter);
    }
}
