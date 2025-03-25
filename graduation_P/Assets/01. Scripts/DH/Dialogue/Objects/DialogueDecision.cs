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
}
