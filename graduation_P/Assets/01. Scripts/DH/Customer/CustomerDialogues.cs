using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class EventDialogue
{
    public string evt;
    public BehaviourType behaviour;
    public DialogueHeader dialogue;
}

[CreateAssetMenu(menuName = "SO/Customer/Dialogues")]
public class CustomerDialogues : ScriptableObject
{
    [Header("list")]
    public List<EventDialogue> events;
    public List<DialogueHeader> enter;
    public List<DialogueHeader> talk;
    public List<DialogueHeader> order;
    public List<DialogueHeader> reaction;
    public List<DialogueHeader> exit;

    [Header("runtime")]
    public List<EventDialogue> events_runtime;
    public List<DialogueHeader> enter_runtime;
    public List<DialogueHeader> talk_runtime;
    public List<DialogueHeader> order_runtime;
    public List<DialogueHeader> reaction_runtime;
    public List<DialogueHeader> exit_runtime;


    public void Initialize()
    {
        events_runtime = new List<EventDialogue>();
        enter_runtime = new List<DialogueHeader>();
        talk_runtime = new List<DialogueHeader>();
        order_runtime = new List<DialogueHeader>();
        reaction_runtime = new List<DialogueHeader>();
        exit_runtime = new List<DialogueHeader>();

        foreach (var i in events)
            events_runtime.Add(new EventDialogue() { evt = i.evt, behaviour = i.behaviour, dialogue = Instantiate(i.dialogue) });
        foreach (var i in enter)
            enter_runtime.Add(Instantiate(i));
        foreach (var i in talk)
            talk_runtime.Add(Instantiate(i));
        foreach (var i in order)
            order_runtime.Add(Instantiate(i));
        foreach (var i in reaction)
            reaction_runtime.Add(Instantiate(i));
        foreach (var i in exit)
            exit_runtime.Add(Instantiate(i));
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

    public DialogueHeader Find(List<DialogueHeader> headers, BehaviourType behaviour, float drunk, float reliance)
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
