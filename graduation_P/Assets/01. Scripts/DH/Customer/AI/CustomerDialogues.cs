using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Customer/Dialogues")]
public class CustomerDialogues : ScriptableObject
{
    public List<DialogueHeader> major;
    public List<DialogueHeader> smallTalk;
    public List<DialogueHeader> defaultTalk;

    public void Initialize()
    {
        for (int i = 0; i < major.Count; i++)
            major[i] = Instantiate(major[i]);
        for (int i = 0; i < smallTalk.Count; i++)
            smallTalk[i] = Instantiate(smallTalk[i]);
        for (int i = 0; i < defaultTalk.Count; i++)
            defaultTalk[i] = Instantiate(defaultTalk[i]);
    }

    public DialogueHeader Query(CustomerFeel feel, BehaviourType behaviour, float drunk, float reliance)
    {
        DialogueHeader result = null;

        List<DialogueHeader> finds = FindAll(major, feel, behaviour, drunk, reliance);
        if (finds.Count > 0)
            result = finds[Random.Range(0, finds.Count)];

        if (result == null)
        {
            finds = FindAll(smallTalk, feel, behaviour, drunk, reliance);
            if (finds.Count > 0)
                result = finds[Random.Range(0, finds.Count)];
        }

        if (result == null)
        {
            finds = FindAll(defaultTalk, feel, behaviour, drunk, reliance);
            if (finds.Count > 0)
                result = finds[Random.Range(0, finds.Count)];
        }

        result.count++;
        return result;
    }

    public List<DialogueHeader> FindAll
        (List<DialogueHeader> headers, CustomerFeel feel, BehaviourType behaviour, float drunk, float reliance)
    {
        return headers.FindAll((header) =>
        {
            if (
                    (header.ignoreFeel || header.feels.Contains(feel)) &&
                    (header.ignoreType || header.behaviour == behaviour) &&
                    (header.ignoreDrunk || header.maxDrunk >= drunk && drunk >= header.minDrunk) &&
                    (header.ignoreReliance || header.maxReliance >= reliance && reliance >= header.minReliance) &&
                    (header.infiniteCount || header.maxCount > header.count)
                )
                return true;
            return false;
        });
    }
}
