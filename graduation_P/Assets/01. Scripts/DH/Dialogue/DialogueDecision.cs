using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Dialogue/Decision")]
public class DialogueDecision : ScriptableObject
{
    [Serializable]
    public class DecisionEffect
    {
        public CustomerStatType type;
        public int value;
    }

    public string text;
    public List<DecisionEffect> effects;
    public DialogueText nextText;

    public void ApplyEffect(Customer customer)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            switch (effects[i].type)
            {
                case CustomerStatType.Drunk:
                    customer.stats.drunk += effects[i].value;
                    break;
                case CustomerStatType.Reliance:
                    customer.stats.reliance += effects[i].value;
                    break;
                case CustomerStatType.Like:
                    customer.stats.like += effects[i].value;
                    break;
            }
        }
    }
}
