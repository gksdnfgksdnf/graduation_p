using System.Collections.Generic;
using UnityEngine;

public enum BehaviourType
{
    Enter,
    Talk,
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

    public virtual void Load()
    {
        information.Load();
    }

    public virtual void Entered()
    {
        information.visitCount++;
    }

    public virtual void Exited()
    {
    }

    public virtual AIBehaviour GetBehaviour() // return aibehaviour by infomation and taste
    {
        AIBehaviour behaviour = new();
        DecideNextBehaviour(behaviour);
        prevBehaviours.Add(behaviour);
        return behaviour;
    }

    protected abstract void DecideNextBehaviour(AIBehaviour behaviour);
    protected abstract bool DecideVisit(int day);

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
