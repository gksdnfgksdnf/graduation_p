using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Dialogue/Decision")]
public class DialogueDecision : DialogueObject
{
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
        public List<SpacialEvent> events;
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
