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
        public float value;
    }

    public string text;
    public List<DecisionEffect> effects;
    public DialogueText nextText;
}
