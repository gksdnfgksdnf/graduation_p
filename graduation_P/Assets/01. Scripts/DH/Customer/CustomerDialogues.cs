using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class EventDialogue
{
    public string evt;
    public DialogueHeader dialogue;
}

[CreateAssetMenu(menuName = "SO/Customer/Dialogues")]
public class CustomerDialogues : ScriptableObject
{
    public List<EventDialogue> events;
    public List<DialogueHeader> dialogues;

    public List<EventDialogue> events_runtime;
    public List<DialogueHeader> dialogues_runtime;


    public void Initialize()
    {
        events_runtime = new List<EventDialogue>();
        dialogues_runtime = new List<DialogueHeader>();

        foreach (var i in events)
            events_runtime.Add(new EventDialogue() { evt = i.evt, dialogue = Instantiate(i.dialogue) });
        foreach (var i in dialogues)
            dialogues_runtime.Add(Instantiate(i));
    }

    public DialogueHeader Query(float drunk, float reliance)
    {
        DialogueHeader result = Find(dialogues_runtime, drunk, reliance);
        result.count++;
        return result;
    }

    public DialogueHeader Find(List<DialogueHeader> headers, float drunk, float reliance)
    {
        var result = headers.FindAll((header) =>
        {
            if (
                    header.maxDrunk >= drunk && drunk >= header.minDrunk &&
                    header.maxReliance >= reliance && reliance >= header.minReliance &&
                    (header.infiniteCount || header.maxCount > header.count)
                )
                return true;
            return false;
        });
        return result[Random.Range(0, result.Count)];
    }
}
