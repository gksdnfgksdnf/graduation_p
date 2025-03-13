using System.Collections.Generic;
using UnityEngine;

public enum BehaviourType
{
    Enter,
    Talking,
    Order,
    Reaction,
    Exit
}

public enum CustomerFeel
{
    Terrible,
    Bad,
    Find,
    Good,
    Perfect
}

public enum CustomerStatType
{
    like,
    reliance,
    progress,
    // temporal value
    drunk,
}

public class AIBehaviour
{
    public CustomerFeel feel;
    public BehaviourType dialogueType;
    public DialogueText dialogue;
}

public abstract class CustomerAI : MonoBehaviour
{
    public CustomerInfomation information;
    public CustomerTaste taste;
    public CustomerDialogues dialogues;

    public List<AIBehaviour> prevBehaviours;

    protected bool enter = false;
    protected int drunk = 0;

    public virtual void Load()
    {
        information.Load();
    }

    public virtual void Entered()
    {
        drunk = 0;
        enter = true;

        information.visitCount++;
    }

    public virtual void Exited()
    {
        enter = false;
    }

    public virtual AIBehaviour GetBehaviour() // return aibehaviour by infomation and taste
    {
        AIBehaviour behaviour = new();
        DecideDialogueType(behaviour);
        DecideNextDialogue(behaviour);
        prevBehaviours.Add(behaviour);
        return behaviour;
    }

    protected abstract void DecideNextDialogue(AIBehaviour behaviour);
    protected abstract void DecideDialogueType(AIBehaviour behaviour);
    protected abstract void DecideVisit(int day);

    public void AddDecision(DialogueDecision decision) // fix information by decisions
    {
        foreach (var effect in decision.effects)
        {
            information.AddEffect(effect);
        }
    }

    public void AddCocktail(CocktailDataSO cocktail) // fix information by taste
    {

    }
}
