using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum DayPhase
{
    Sunset,
    Midnight,
    Dreamy
}

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    public List<Func<UniTask>> onDayEnter = new();
    public List<Func<UniTask>> onDayExit = new();
    public List<Func<DayPhase, UniTask>> onPhase = new();
    public List<Func<int, UniTask>> onHour = new();

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
        DayCycle();
    }

    private async void DayCycle()
    {
        hour = 18;
        phase = DayPhase.Sunset;
        await UniTask.WhenAll(onDayEnter.Select(t => t()));

        for (int i = 0; i < 3; i++)
        {
            phase = (DayPhase)i;
            await UniTask.WhenAll(onPhase.Select(t => t(phase)));
            for (int j = 0; j < 3; j++)
            {
                TimerUI.Instance.SetHour(hour % 24);
                TimerUI.Instance.SetMinute(0f);
                bool timer = false;
                await UniTask.WhenAll(onHour.Select(t => t(hour)));
                Timer.StartTimer(timeMultiplier, () =>
                {
                    timer = true;
                    hour++;
                });
                await UniTask.WaitUntil(() => timer);
            }
        }

        TimerUI.Instance.SetHour(3);
        TimerUI.Instance.SetMinute(0f);

        day++;
        await UniTask.WhenAll(onDayExit.Select(t => t()));
    }
}
