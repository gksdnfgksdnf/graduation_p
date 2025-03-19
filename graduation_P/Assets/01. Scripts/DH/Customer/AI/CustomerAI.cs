using System;
using System.Collections.Generic;
using UnityEngine;
using static DialogueDecision;

public enum BehaviourType
{
    None,
    Enter,
    Talk,
    Order,
    Reaction,
    Exit
}

public enum CustomerFeel
{
    None,
    Terrible,
    Bad,
    Fine,
    Good,
    Perfect
}

public enum SpacialEvent
{
    None,
    Order, // 다음 행동은 주문
    FirstOrder, // 다음 행동은 첫 주문
    FavoriteOrder, // 다음 행동은 특별한 주문
    GetOut, // 다음 행동은 나감
    FirstGetOut, // 다음 행동은 나가고 다신 돌아오지 않음
    FirstVisit, // 첫 인사를 함
}

[Serializable]
public class SpacialBehaviour
{
    public SpacialEvent type;
    public AIBehaviour behaviour;
}

[Serializable]
public class AIBehaviour
{
    public CustomerFeel feel;
    public BehaviourType behaviour;
    public DialogueHeader dialogue;

    public override string ToString()
    {
        return $"{feel}-{behaviour}: {dialogue.name}";
    }
}

public abstract class CustomerAI : MonoBehaviour
{
    public CustomerInfomation information;
    public CustomerTaste taste;
    public CustomerDialogues dialogues;

    public List<SpacialBehaviour> spacials;
    public List<AIBehaviour> prevBehaviours;

    public AIBehaviour next = new();

    public virtual void Entered()
    {
        information.visitCount++;
        if (information.visitCount == 1)
            HandleDecisionEvent(SpacialEvent.FirstVisit);
    }

    public virtual void Exited()
    {
    }

    public virtual AIBehaviour GetBehaviour() // return aibehaviour by infomation and taste
    {
        AIBehaviour behaviour = DecideNextBehaviour();
        prevBehaviours.Add(behaviour);
        return behaviour;
    }

    public virtual void AddDecision(Decision decision) // modify information by decisions
    {
        foreach (var effect in decision.effects)
            information.AddEffect(effect);
        foreach (var evt in decision.events)
            HandleDecisionEvent(evt);
    }

    public abstract void AddCocktail(CocktailDataSO cocktail);
    protected abstract AIBehaviour DecideNextBehaviour();
    protected abstract void HandleDecisionEvent(SpacialEvent evt);
    public abstract bool DecideVisit(int day);
}
