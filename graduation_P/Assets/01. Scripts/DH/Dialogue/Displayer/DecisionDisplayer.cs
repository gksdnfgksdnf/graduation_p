using System.Collections.Generic;
using UnityEngine;
using static DialogueDecision;

public class DecisionDisplayer : MonoBehaviour
{
    public List<DecisionButton> buttons;

    private bool isShow = false;
    private Customer customer;
    private DialogueDisplayer displayer;

    public void Init()
    {
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].Init();
    }

    public bool IsShowDecisions()
    {
        return isShow;
    }

    public void Show(Customer customer, DialogueDecision decision, DialogueDisplayer displayer)
    {
        for (int i = 0; i < decision.decisions.Count; i++)
        {
            buttons[i].Active(decision.decisions[i], this);
        }
        isShow = true;
        this.customer = customer;
        this.displayer = displayer;
    }

    public void Unshow()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Inactive();
        }
        isShow = false;
        customer = null;
        displayer = null;
    }

    public void Select(Decision decision)
    {
        customer.AI.AddDecision(decision);

        if (decision.next == null)
        {
            DialogueManager.Instance.ExitDialogue();
            return;
        }

        DialogueManager.Instance.EnterDialogue(customer, decision.next, displayer);
    }
}
