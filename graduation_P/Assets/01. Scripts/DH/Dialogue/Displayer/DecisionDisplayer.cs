using System.Collections.Generic;
using UnityEngine;

public class DecisionDisplayer : MonoBehaviour
{
    public List<DecisionButton> buttons;

    private bool isShow = false;
    private Customer customer;

    public void Init()
    {
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].Init();
    }

    public bool IsShowDecisions()
    {
        return isShow;
    }

    public void Show(Customer customer, List<DialogueDecision> decisions)
    {
        for (int i = 0; i < decisions.Count; i++)
        {
            buttons[i].Active(decisions[i], this);
        }
        isShow = true;
        this.customer = customer;
    }

    public void Unshow()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Inactive();
        }
        isShow = false;
        customer = null;
    }

    public void Select(DialogueDecision decision)
    {
        customer.AI.AddDecision(decision);

        if (decision.nextText == null)
        {
            DialogueManager.Instance.ExitDialogue();
            return;
        }

        DialogueManager.Instance.EnterDialogue(customer, decision.nextText);
        Unshow();
    }
}
