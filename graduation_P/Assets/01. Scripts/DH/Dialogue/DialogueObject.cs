using System;
using UnityEngine;

public abstract class DialogueObject : ScriptableObject
{
    public DialogueType type;

    public abstract void Dialogue(Customer customer, DialogueEventWaiter waiter);
}
