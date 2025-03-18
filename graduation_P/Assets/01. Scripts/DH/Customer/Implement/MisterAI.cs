using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MisterAI : CustomerAI
{
    [Header("Temporal Values")]
    protected bool enter = false;
    protected bool reaction = false;
    protected float dayFeeling = 0;
    protected int talkCount = 0;
    protected int orderCount = 0;

    public override void Entered()
    {
        enter = true;
        information.drunk = 0;
        dayFeeling = 0;
        talkCount = 0;
        orderCount = 0;

        base.Entered();
    }

    protected override AIBehaviour DecideNextBehaviour()
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

    protected override void HandleDecisionEvent(SpacialEvent evt)
    {
        switch (evt)
        {
            case SpacialEvent.None:
                break;
            case SpacialEvent.Order:
                next = new AIBehaviour() { behaviour = BehaviourType.Order };
                break;
            case SpacialEvent.FirstOrder:
                next = spacials.First(s => s.type == SpacialEvent.FirstOrder)?.behaviour;
                break;
            case SpacialEvent.FavoriteOrder:
                next = spacials.First(s => s.type == SpacialEvent.FavoriteOrder)?.behaviour;
                break;
            case SpacialEvent.GetOut:
                next = new AIBehaviour() { behaviour = BehaviourType.Exit };
                break;
            case SpacialEvent.FirstGetOut:
                information.caseClosed = true;
                next = new AIBehaviour() { feel = CustomerFeel.Terrible, behaviour = BehaviourType.Exit };
                break;
        }
    }

    private DialogueHeader DecideDialogue(AIBehaviour behaviour)
    {
        return dialogues.Query(
            behaviour.feel,
            behaviour.behaviour,
            information.drunk,
            information.reliance,
            true
        );
    }
    private BehaviourType DecideBehaviourType()
    {
        if (enter)
        {
            enter = false;
            return BehaviourType.Enter;
        }
        if (reaction)
        {
            reaction = false;
            return BehaviourType.Reaction;
        }
        if (information.drunk >= 90)
            return BehaviourType.Exit;

        if (orderCount >= talkCount)
            return BehaviourType.Talk;
        else
            return BehaviourType.Order;
    }
    private CustomerFeel DecideFeel()
    {
        CustomerFeel feel = CustomerFeel.Fine;

        float feelValue = information.reliance;
        feelValue += UnityEngine.Random.Range(-5f, 5);
        feelValue += dayFeeling;

        if (feelValue >= 65f)
            feel = CustomerFeel.Perfect;
        else if (feelValue >= 40f)
            feel = CustomerFeel.Good;
        else if (feelValue >= 0f)
            feel = CustomerFeel.Fine;
        else if (feelValue >= -40f)
            feel = CustomerFeel.Bad;
        else
            feel = CustomerFeel.Terrible;

        return feel;
    }
    public override bool DecideVisit(int day)
    {
        if (information.caseClosed)
            return false;

        int visitValue = 0;
        visitValue += day - information.firstAppearDay + 50;
        return visitValue >= Random.Range(0, 100 - information.reliance);
    }
    public override void AddCocktail(CocktailDataSO cocktail)
    {
        information.drunk += 20;
        reaction = true;
    }
}
