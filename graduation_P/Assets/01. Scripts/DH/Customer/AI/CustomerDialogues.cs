using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Customer/Dialogues")]
public class CustomerDialogues : ScriptableObject
{
    public List<DialogueHeader> major;
    public List<DialogueHeader> smallTalk;
    public List<DialogueHeader> etc;

    private void Awake()
    {
        for (int i = 0; i < major.Count; i++)
            major[i] = Instantiate(major[i]);
        for (int i = 0; i < smallTalk.Count; i++)
            smallTalk[i] = Instantiate(smallTalk[i]);
        for (int i = 0; i < etc.Count; i++)
            etc[i] = Instantiate(etc[i]);
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
            finds = FindAll(etc, feel, behaviour, drunk, reliance);
            if (finds.Count > 0)
                result = finds[Random.Range(0, finds.Count)];
        }

        if (result == null)
        {
            result = etc[Random.Range(0, etc.Count)];
            Debug.Log("return random dialogue!");
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
                    (header.feel == feel || header.ignoreFeel) &&
                    (header.behaviour == behaviour || header.ignoreType) &&
                    (header.maxDrunk > drunk && drunk >= header.minDrunk || header.ignoreDrunk) &&
                    (header.maxReliance > reliance && reliance >= header.minReliance || header.ignoreReliance) &&
                    (header.count >= header.maxCount || header.infiniteCount)
                )
                return true;
            return false;
        });
    }
}
