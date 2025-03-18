using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance { get; private set; }

    public List<Customer> customers = new();
    public Customer visitor;

    [SerializeField] private bool test = true;

    private void Awake()
    {
        Instance = this;
        if (test)
        {
            EnterCustomer(GetRandomCustomer(0));
        }
    }

    public Customer GetRandomCustomer(int day)
    {
        List<Customer> visitor = customers.FindAll(visit => visit.AI.DecideVisit(day));
        if (visitor.Count > 0)
            return visitor[Random.Range(0, visitor.Count)];
        else
            return customers[Random.Range(0, customers.Count)];
    }

    public void EnterCustomer(Customer customer)
    {
        visitor = customer;
        visitor.Enter();
    }

    public void ExitCustomer()
    {
        visitor.Exit();
        visitor = null;
    }
}
