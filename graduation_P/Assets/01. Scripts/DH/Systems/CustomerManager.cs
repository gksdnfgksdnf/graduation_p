using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueType
{
    Don,
}

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance { get; private set; }

    public List<Customer> Customers = new();
    public List<Customer> Visitors = new();

    private void Awake()
    {
        Instance = this;
    }

    public async UniTask PhaseUpdate(int day, DayPhase phase)
    {
        var newCus = Customers.FindAll(cus => !Visitors.Contains(cus) && cus.Visit(day, phase));
        foreach (var cus in newCus)
            await cus.Enter();

        Visitors.AddRange(newCus);
    }

    public Customer FindVisitor(DialogueType customer)
    {
        foreach (var cus in Visitors)
            if (cus.type == customer)
                return cus;
        return null;
    }

    public async UniTask HourUpdate(DayPhase phase, int hour)
    {
        foreach (var cus in Visitors)
        {
            await DialogueManager.Instance.PlayDialogue(cus.Dialogue());
        }
    }
}
