using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public static TimerUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI hourText;
    [SerializeField] private TextMeshProUGUI minuteText;

    private void Awake()
    {
        Instance = this;
    }

    public void SetHour(float hour)
    {
        hourText.text = ((int)hour).ToString();
    }

    public void SetMinute(float minute)
    {
        minuteText.text = ((int)minute).ToString();
    }
}
