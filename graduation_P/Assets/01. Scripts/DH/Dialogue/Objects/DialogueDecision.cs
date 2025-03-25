using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DecisionEffect
{
    public EffectType type;
    public float value;
}

[Serializable]
public class Decision
{
    public string text;
    public List<DecisionEffect> effects;
    public DialogueObject next;
}

[CreateAssetMenu(menuName = "DialogueDecision")]
public class DialogueDecision : DialogueObject
{
    public List<Decision> decisions;

    private Customer customer;
    private DialogueEventWaiter waiter;

    public override void Dialogue(Customer customer, DialogueEventWaiter waiter)
    {
        this.customer = customer;
        this.waiter = waiter;

        DecisionManager.Instance.Enable(true);
        DecisionDisplayer displayer = DecisionManager.Instance.displayer;
        displayer.onSelected += HandleSelect;
        displayer.Show(decisions);
    }

    private void HandleSelect(Decision decision)
    {
        DecisionManager.Instance.Enable(false);
        customer.AddDecision(decision);
        waiter.next = decision.next;
        waiter.IsCompleted = true;
    }
}
