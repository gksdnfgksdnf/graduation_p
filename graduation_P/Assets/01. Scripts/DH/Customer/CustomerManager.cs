using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance { get; private set; }

    public List<Customer> Customers = new();
    public List<Customer> Visitors = new();

    public void PhaseUpdate(int day, DayPhase phase)
    {
        Visitors.AddRange(Customers.FindAll(cus =>
        {
            if (!Visitors.Contains(cus) && cus.Visit(day, phase))
            {
                cus.Enter();
                return true;
            }
            return false;
        }));
    }

    public void HourUpdate(DayPhase phase, int hour)
    {
        StartCoroutine(VisitorUpdate());
    }

    private IEnumerator VisitorUpdate()
    {
        DayManager.Instance.Timer.PauseTimer(true);
        foreach (var cus in Visitors)
        {
            yield return null;
        }
        DayManager.Instance.Timer.PauseTimer(false);
    }
}
