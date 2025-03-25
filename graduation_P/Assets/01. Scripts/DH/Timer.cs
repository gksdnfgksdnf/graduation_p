using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time = 0f;
    public float timeMultiplier = 1f;
    public bool timeStarted = false;
    public bool timePaused = false;

    public Action onTimerEnd = null;
    private Action callback = null;

    public void StartTimer(float timeMultiplier, Action callback = null)
    {
        time = 0;
        this.timeMultiplier = timeMultiplier;
        this.callback = callback;
        timeStarted = true;
    }

    public void StopTimer()
    {
        time = 0;
        callback = null;
        timeStarted = false;
    }

    public void PauseTimer(bool paused)
    {
#if UNITY_EDITOR
        if (timePaused == paused)
        {
            if (timePaused)
                Debug.Log("[TimeManager] Time Already Paused");
            else
                Debug.Log("[TimeManager] Time Already Started");
        }
#endif
        timePaused = paused;
    }

    private void Update()
    {
        if (!timePaused && timeStarted)
        {
            time += Time.deltaTime * timeMultiplier;
            if (time >= 20f)
            {
                callback?.Invoke();
                onTimerEnd?.Invoke();
                StopTimer();
            }
            TimerUI.Instance.SetMinute(time);
        }
    }
}
