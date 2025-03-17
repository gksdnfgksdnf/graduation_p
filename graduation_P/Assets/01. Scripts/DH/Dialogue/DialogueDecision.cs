using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Dialogue/Decision")]
public class DialogueDecision : DialogueObject
{
    [Serializable]
    public class DecisionEffect
    {
        public CustomerStatType type;
        public float value;
    }

    [Serializable]
    public class Decision
    {
        public string text;
        public List<DecisionEffect> effects;
        public DialogueObject next;
    }
    public List<Decision> decisions;

    public override void EnterDialogue(Customer customer, DialogueDisplayer displayer)
    {
        displayer.DecisionDisplayer.Show(customer, this, displayer);
    }

    public override void ExitDialogue(Customer customer, DialogueDisplayer displayer)
    {
        displayer.DecisionDisplayer.Unshow();
    }
}
