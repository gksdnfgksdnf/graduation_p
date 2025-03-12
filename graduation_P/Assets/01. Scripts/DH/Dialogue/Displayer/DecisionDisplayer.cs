using System.Collections.Generic;
using UnityEngine;

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

    public void Show(Customer customer, DialogueDisplayer displayer, List<DialogueDecision> decisions)
    {
        for (int i = 0; i < decisions.Count; i++)
        {
            buttons[i].Active(decisions[i], this);
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

    public void Select(DialogueDecision decision)
    {
        decision.ApplyEffect(customer);

        if (decision.nextText == null)
        {
            DialogueManager.Instance.ExitDialogue();
            return;
        }
        DialogueManager.Instance.EnterDialogue(customer, displayer, decision.nextText);
        Unshow();
    }
}
