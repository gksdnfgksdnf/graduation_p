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

public enum EnterEventType
{
    None, // 85%
    Drunk, // 10%
    TerribleDay, // 5%
}

public class AIBehaviour
{
    public CustomerFeel feel;
    public BehaviourType behaviour;
    public DialogueText dialogue;
    public string order;
}

public abstract class CustomerAI : MonoBehaviour
{
    public CustomerInfomation information;
    public CustomerTaste taste;
    public CustomerDialogues dialogues;

    public List<AIBehaviour> prevBehaviours;

    public virtual void Load()
    {
        _ = information.Load();
    }
    public virtual void Save()
    {
        _ = information.Save();
    }

    public virtual void Entered(EnterEventType enterEvt)
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

    public virtual void AddDecision(DialogueDecision decision) // modify information by decisions
    {
        foreach (var effect in decision.effects)
        {
            information.AddEffect(effect);
        }
    }

    public abstract void AddCocktail(CocktailDataSO cocktail);
    protected abstract void DecideNextBehaviour(AIBehaviour behaviour);
    protected abstract int DecideVisit(int day);
}
