using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueText")]
public class DialogueText : DialogueObject // default text dialogue
{
    public List<string> texts;
    public DialogueObject next;
}
