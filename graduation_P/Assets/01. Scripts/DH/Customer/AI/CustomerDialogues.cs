using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Customer/Dialogues")]
public class CustomerDialogues : ScriptableObject
{
    public List<DialogueHeader> major;
    public List<DialogueHeader> minor;
    public List<DialogueHeader> smallTalk;
    public List<DialogueHeader> etc;

    public float drunkDistance = 20f;
    public float relianceDistance = 20f;

    public DialogueHeader Query(CustomerFeel feel, BehaviourType behaviour, float drunk, float reliance, bool findClose = false)
    {
        DialogueHeader result = null;

        List<DialogueHeader> finds = FindAll(major, feel, behaviour, drunk, reliance, findClose);
        if (finds.Count > 0)
            result = finds[Random.Range(0, finds.Count)];
        else
            finds = FindAll(minor, feel, behaviour, drunk, reliance, findClose);

        if (finds.Count > 0 && result == null)
            result = finds[Random.Range(0, finds.Count)];
        else
            finds = FindAll(smallTalk, feel, behaviour, drunk, reliance, false);

        if (finds.Count > 0 && result == null)
            result = finds[Random.Range(0, finds.Count)];
        else
            result = etc[Random.Range(0, etc.Count)];

        result.count++;

        return result;
    }

    public List<DialogueHeader> FindAll(List<DialogueHeader> headers, CustomerFeel feel, BehaviourType behaviour, float drunk, float reliance, bool findClose = false)
    {
        return headers.FindAll((header) =>
        {
            if (
                    header.feel == feel &&
                    header.behaviour == behaviour &&
                    header.drunk <= drunk &&
                    header.reliance <= reliance
                )
            {
                if (header.count >= header.maxCount)
                    return false;

                if (findClose)
                    if (drunk - header.drunk > drunkDistance || reliance - header.reliance > relianceDistance)
                        return false;

                return true;
            }
            return false;
        });
    }
}
