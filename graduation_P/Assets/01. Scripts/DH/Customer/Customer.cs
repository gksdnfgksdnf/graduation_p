using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerAI AI;

    public List<Customer> customers;

    private void Awake()
    {
        if (AI == null)
            AI = GetComponent<CustomerAI>();
    }

    public void Load()
    {
        AI.Load();
    }
}
