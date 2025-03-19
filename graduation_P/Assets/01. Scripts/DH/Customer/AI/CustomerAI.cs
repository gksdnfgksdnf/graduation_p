using System;
using System.Collections.Generic;
using System.Linq;
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

public enum SpecialEvent
{
    None,
    CaseClose, // 끝남
    FirstOrder, // 다음 행동은 첫 주문
    FirstVisit, // 첫 인사를 함
}

[Serializable]
public class SpecialBehaviour
{
    public string name;
    public SpecialEvent type;
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

    public List<SpecialBehaviour> specials;
    public List<AIBehaviour> prevBehaviours;

    public AIBehaviour next = new();
    public CocktailDataSO ordered = null;
    public CocktailDataSO served = null;

    protected int talkCount = 0;
    protected int orderCount = 0;

    private void Awake()
    {
        information = Instantiate(information);
    }

    public virtual void Entered()
    {
        information.visitCount++;
        information.drunk = 0;
        next = null;
        ordered = null;
        served = null;
        talkCount = 0;
        orderCount = 0;

        if (information.visitCount == 1)
            SetSpecialEvent(SpecialEvent.FirstVisit);
        else
            SetBehaviourType(BehaviourType.Enter);
    }
    public virtual void Exited()
    {
        next = null;
        ordered = null;
        served = null;
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
        SetBehaviourType(decision.nextBehaviour);
        SetSpecialEvent(decision.specialEvt);
    }

    public virtual void OrderCocktail(CocktailDataSO cocktail)
    {
        ordered = cocktail;
    }
    public virtual void ServeCocktail(CocktailDataSO cocktail)
    {
        served = cocktail;
        SetBehaviourType(BehaviourType.Reaction);
    }

    public virtual void SetBehaviourType(BehaviourType evt) // set next behaviour type
    {
        switch (evt)
        {
            case BehaviourType.Enter:
                if (next == null)
                    next = new AIBehaviour() { behaviour = BehaviourType.Enter };
                break;
            case BehaviourType.Talk:
                if (next == null)
                    next = new AIBehaviour() { behaviour = BehaviourType.Talk };
                break;
            case BehaviourType.Order:
                if (next == null)
                    next = new AIBehaviour() { behaviour = BehaviourType.Order };
                break;
            case BehaviourType.Reaction:
                if (next == null)
                    next = new AIBehaviour() { behaviour = BehaviourType.Reaction };
                break;
            case BehaviourType.Exit:
                if (next == null)
                    next = new AIBehaviour() { behaviour = BehaviourType.Exit };
                break;
        }
    }
    public virtual void SetSpecialEvent(SpecialEvent evt) // handle evt
    {
        switch (evt)
        {
            case SpecialEvent.CaseClose:
                information.caseClosed = true;
                next = specials.First(s => s.type == SpecialEvent.CaseClose).behaviour;
                break;
            case SpecialEvent.FirstOrder:
                next = specials.First(s => s.type == SpecialEvent.FirstOrder).behaviour;
                break;
            case SpecialEvent.FirstVisit:
                next = specials.First(s => s.type == SpecialEvent.FirstVisit).behaviour;
                break;
        }
    }

    #region DecideBehaviour
    protected virtual AIBehaviour DecideNextBehaviour()
    {
        if (next == null)
            next = new();
        AIBehaviour behaviour = next;
        next = null;

        if (behaviour.feel == CustomerFeel.None)
            behaviour.feel = DecideFeel();
        if (behaviour.behaviour == BehaviourType.None)
            behaviour.behaviour = DecideBehaviourType();
        if (behaviour.dialogue == null)
            behaviour.dialogue = DecideDialogue(behaviour);
        return behaviour;
    }
    protected virtual DialogueHeader DecideDialogue(AIBehaviour behaviour)
    {
        return dialogues.Query(
            behaviour.feel,
            behaviour.behaviour,
            information.drunk,
            information.reliance
        );
    }
    protected virtual BehaviourType DecideBehaviourType()
    {
        if (information.drunk >= 90)
            return BehaviourType.Exit;

        if (ordered != null)
            return BehaviourType.Talk;
        else
            return BehaviourType.Order;
    }
    protected virtual CustomerFeel DecideFeel()
    {
        CustomerFeel feel = CustomerFeel.Fine;

        float feelValue = information.reliance;
        feelValue += UnityEngine.Random.Range(-5f, 5);

        if (feelValue >= 60f)
            feel = CustomerFeel.Perfect;
        else if (feelValue >= 20f)
            feel = CustomerFeel.Good;
        else if (feelValue >= -20f)
            feel = CustomerFeel.Fine;
        else if (feelValue >= -60f)
            feel = CustomerFeel.Bad;
        else
            feel = CustomerFeel.Terrible;
        return feel;
    }
    #endregion

    public abstract bool DecideVisit(int day);
}
