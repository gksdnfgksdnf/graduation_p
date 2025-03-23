using System.Collections;
using UnityEngine;

public enum DayPhase
{
    Sunset,
    Midnight,
    Dreamy
}
//public enum Days
//{
//    Mon,
//    Tue,
//    Wed,
//    Thu,
//    Fri,
//    Sat,
//    Sun
//}

public class GameManager : MonoBehaviour
{
    public int day = 0;
    public DayPhase phase = DayPhase.Sunset;
}
