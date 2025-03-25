using UnityEngine;

public abstract class DialogueObject : ScriptableObject
{
    public virtual void EnterDialogue(Customer customer) { }
    public virtual void ExitDialogue(Customer customer) { }
}
