using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DayPhase
{
    Shadow,
    Midnight,
    Dreamy
}

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    public List<Action<EventWaiter>> onDayEnter = new();
    public List<Action<EventWaiter>> onDayExit = new();
    public List<Action<EventWaiter>> onPhase = new();
    public List<Action<EventWaiter>> onHour = new();

    public Timer Timer { get; private set; }
    public float timeMultiplier = 2f;

    public int day = 1;
    public DayPhase phase = DayPhase.Shadow;
    public int hour = 0;

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
        hour = 0;
        phase = DayPhase.Shadow;

        yield return StartCoroutine(EventWaiter.WaitInvoker(onDayEnter));

        for (int i = 0; i < 3; i++)
        {
            phase = (DayPhase)i;
            yield return StartCoroutine(EventWaiter.WaitInvoker(onPhase));

            for (int j = 0; j < 3; j++)
            {
                TimerUI.Instance.SetHour(hour % 24);
                TimerUI.Instance.SetMinute(0f);
                Debug.Log("Hour Event Start");
                yield return StartCoroutine(EventWaiter.WaitInvoker(onHour));
                Debug.Log("Hour Event End");

                bool timer = false;
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
        yield return StartCoroutine(EventWaiter.WaitInvoker(onDayExit));
        day++;
    }
}
