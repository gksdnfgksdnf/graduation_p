using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance { get; private set; }

    public List<Customer> customerPrefabs = new List<Customer>();

    private void Awake()
    {
        Instance = this;
        Enter();
    }

    public void Enter()
    {
        foreach (var customerPrefab in customerPrefabs)
        {
            Customer customer = Instantiate(customerPrefab);
            customer.Load();
        }
    }
}
