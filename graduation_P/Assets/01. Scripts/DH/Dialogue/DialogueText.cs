using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Dialogue/Text")]
public class DialogueText : ScriptableObject // default text dialogue
{
    public DialogueManager.DialogueType type = DialogueManager.DialogueType.Customer;
    public List<string> texts;
    public List<DialogueDecision> decisions;
    public DialogueText next;
}
