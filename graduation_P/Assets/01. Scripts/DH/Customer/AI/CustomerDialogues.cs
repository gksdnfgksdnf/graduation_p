using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Customer/Dialogues")]
public class CustomerDialogues : ScriptableObject
{
    public List<DialogueText> enter = new();
    public List<DialogueText> talk = new();
    public List<DialogueText> order = new();
    public List<DialogueText> reaction = new();
    public List<DialogueText> exit = new();

    public DialogueText Query(CustomerFeel feel, BehaviourType behaviour, float reliance, int mindBarrier)
    {
        return null;
    }
}
