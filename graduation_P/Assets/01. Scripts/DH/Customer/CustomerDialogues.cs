using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Customer/Dialogues")]
public class CustomerDialogues : ScriptableObject
{
    public List<DialogueHeader> headers;

    private List<DialogueHeader> enter;
    private List<DialogueHeader> talk;
    private List<DialogueHeader> order;
    private List<DialogueHeader> reaction;
    private List<DialogueHeader> exit;

    public void Initialize()
    {
        foreach (var header in headers)
        {
            switch (header.behaviour)
            {
                case BehaviourType.Enter:
                    enter.Add(header);
                    break;
                case BehaviourType.Talk:
                    talk.Add(header);
                    break;
                case BehaviourType.Order:
                    order.Add(header);
                    break;
                case BehaviourType.Reaction:
                    reaction.Add(header);
                    break;
                case BehaviourType.Exit:
                    exit.Add(header);
                    break;
            }
        }
    }

    public DialogueHeader Query(BehaviourType behaviour, float drunk, float reliance)
    {
        DialogueHeader result = null;

        switch (behaviour)
        {
            case BehaviourType.Enter:
                result = Find(enter, behaviour, drunk, reliance);
                break;
            case BehaviourType.Talk:
                result = Find(talk, behaviour, drunk, reliance);
                break;
            case BehaviourType.Order:
                result = Find(order, behaviour, drunk, reliance);
                break;
            case BehaviourType.Reaction:
                result = Find(reaction, behaviour, drunk, reliance);
                break;
            case BehaviourType.Exit:
                result = Find(exit, behaviour, drunk, reliance);
                break;
        }

        result.count++;
        return result;
    }

    public DialogueHeader Find
        (List<DialogueHeader> headers, BehaviourType behaviour, float drunk, float reliance)
    {
        headers.FindAll((header) =>
        {
            if (
                    header.behaviour == behaviour &&
                    header.maxDrunk >= drunk && drunk >= header.minDrunk &&
                    header.maxReliance >= reliance && reliance >= header.minReliance &&
                    (header.infiniteCount || header.maxCount > header.count)
                )
                return true;
            return false;
        });
        return headers[Random.Range(0, headers.Count)];
    }
}
