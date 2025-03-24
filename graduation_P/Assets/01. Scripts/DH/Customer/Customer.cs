using System;
using System.Linq;
using UnityEngine;

public enum BehaviourType
{
    None,
    Enter,
    Talk,
    Order,
    Reaction,
    Exit,
}

public class CustomerBehaviour
{
    public BehaviourType type;
    public DialogueObject dialogue;
}

public abstract class Customer : MonoBehaviour
{
    public CustomerAnimator Animator;
    public CustomerDialoguer Dialoguer;
    public CustomerDialogues Dialogues;
    public CustomerInformation Infomation;
    public CustomerTaste Taste;

    protected virtual void Awake()
    {
        Dialoguer.ExitDialogue();
        Dialogues.Initialize();
    }

    public virtual void Enter()
    {
        Animator.Enter();
    }

    public virtual void Exit()
    {
        Animator.Exit();
    }

    public virtual void Talk(BehaviourType behaviour, Action callback)
    {
        DialogueHeader dialogue = Dialogues.Query(behaviour, Infomation.drunk, Infomation.reliance);
        Dialoguer.EnterDialogue(this, dialogue.header);
    }

    public virtual void Talk(DialogueHeader dialogue, Action callback)
    {
        Dialoguer.EnterDialogue(this, dialogue.header);
    }

    public virtual void Talk(string evt, Action callback)
    {
        DialogueHeader dialogue = Dialogues.events_runtime.FirstOrDefault(evtHeader => evtHeader.evt == evt).dialogue;
        Dialoguer.EnterDialogue(this, dialogue.header);
    }

    public abstract bool Visit(int day, DayPhase phase);
}
