using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Customer/Dialogues")]
public class CustomerDialogues : ScriptableObject
{
    public List<DialogueHeader> headers;

    public DialogueHeader Query(CustomerFeel feel, BehaviourType behaviour, float drunk, float reliance)
    {
        List<DialogueHeader> finds = headers.FindAll((header) =>
        {
            if (
                    header.feel == feel &&
                    header.behaviour == behaviour &&
                    header.drunk <= drunk &&
                    header.reliance <= reliance
                )
                return true;
            return false;
        });

        if (finds.Count > 0)
            return finds[Random.Range(0, finds.Count)];
        return null;
    }
}
