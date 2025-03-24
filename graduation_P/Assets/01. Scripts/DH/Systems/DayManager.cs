using System.Collections;
using UnityEngine;

public enum DayPhase
{
    Sunset,
    Midnight,
    Dreamy
}

public delegate void DayEvent(int day);
public delegate void PhaseEvent(DayPhase phase);
public delegate void HourEvent(int hour);


public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    public DayEvent onDayEnter;
    public DayEvent onDayExit;
    public PhaseEvent onPhase;
    public HourEvent onHour;

    public Timer Timer { get; private set; }
    public float timeMultiplier = 2f;

    public int day = 1;
    public int hour = 6;
    public DayPhase phase = DayPhase.Sunset;

    private void Awake()
    {
        Instance = this;
        Timer = GetComponent<Timer>();
    }

    public void StartDay()
    {
        StartCoroutine(DayCycle());
    }

    private IEnumerator DayCycle()
    {
        hour = 18;
        phase = DayPhase.Sunset;
        onDayEnter?.Invoke(day);

        for (int i = 0; i < 3; i++)
        {
            phase = (DayPhase)i;
            onPhase?.Invoke(phase);
            for (int j = 0; j < 3; j++)
            {
                TimerUI.Instance.SetHour(hour % 24);
                TimerUI.Instance.SetMinute(0f);
                bool timer = false;
                onHour?.Invoke(hour);
                Timer.StartTimer(timeMultiplier, () =>
                {
                    timer = true;
                    hour++;
                });
                yield return new WaitUntil(() => timer);
            }
        }

        TimerUI.Instance.SetHour(3);
        TimerUI.Instance.SetMinute(0f);

        day++;
        onDayExit?.Invoke(day);
    }
}
