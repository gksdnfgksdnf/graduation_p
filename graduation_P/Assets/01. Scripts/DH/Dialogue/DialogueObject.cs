using UnityEngine;

public abstract class DialogueObject : ScriptableObject
{
    public DialogueManager.SpeakerType type = DialogueManager.SpeakerType.Customer;

    public abstract void EnterDialogue(Customer customer, DialogueDisplayer displayer);
    public abstract void ExitDialogue(Customer customer, DialogueDisplayer displayer);
}
