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

    private void Start()
    {
        DayManager.Instance.onPhase.Add(PhaseUpdate);
    }

    public void PhaseUpdate(EventWaiter waiter)
    {
        DayPhase phase = DayManager.Instance.phase;
        int day = DayManager.Instance.day;

        var newCustomer = Customers.FindAll(cus => !Visitors.Contains(cus) && cus.Visit(day, phase));
        foreach (var customer in newCustomer)
            customer.Enter();
        Visitors.AddRange(newCustomer);

        waiter.IsCompleted = true;
    }

    public Customer FindVisitor(DialogueType customer)
    {
        foreach (var cus in Visitors)
            if (cus.type == customer)
                return cus;
        return null;
    }

    public void HourUpdate(EventWaiter waiter)
    {
        var customer = Visitors[Random.Range(0, Customers.Count)];
        DialogueManager.Instance.PlayDialogue(customer.Dialogue(), waiter);
    }
}
