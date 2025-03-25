using Cysharp.Threading.Tasks;
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

    private async UniTask HandlePhaseChange(DayPhase phase)
    {
        var timer = DayManager.Instance.Timer;
        float originTimeMultiplier = timer.timeMultiplier;

        DayManager.Instance.onPhase.Add(HandlePhaseChange);
        timer.timeMultiplier = 0.05f;
        await CustomerManager.Instance.PhaseUpdate(DayManager.Instance.day, phase);
        timer.timeMultiplier = originTimeMultiplier;
        DayManager.Instance.onPhase.Remove(HandlePhaseChange);
    }

    private async UniTask HandleHourChange(int hour)
    {
        var timer = DayManager.Instance.Timer;
        float originTimeMultiplier = timer.timeMultiplier;

        DayManager.Instance.onHour.Add(HandleHourChange);
        timer.timeMultiplier = 0.05f;
        await CustomerManager.Instance.HourUpdate(DayManager.Instance.phase, hour);
        timer.timeMultiplier = originTimeMultiplier;
        DayManager.Instance.onHour.Remove(HandleHourChange);
    }
}
