using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class DialogueObject : ScriptableObject
{
    public DialogueType type;
    public abstract UniTask<DialogueObject> Dialogue(Customer customer);
}
