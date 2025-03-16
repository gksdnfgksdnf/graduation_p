using Random = UnityEngine.Random;

public class ParanoiaAI : CustomerAI
{
    protected bool enter = false;
    protected bool reaction = false;

    protected int drunk = 0;
    protected float drunkMultiplier = 2f;

    protected float dayFeeling = 0;

    protected int talkCount = 0;
    protected int orderCount = 0;

    public override void Entered(EnterEventType enterEvt)
    {
        enter = true;

        if (enterEvt == EnterEventType.Drunk)
            drunk = Random.Range(40, 60);
        else
            drunk = 0;

        if (enterEvt == EnterEventType.TerribleDay)
            dayFeeling = -33f; // 멘탈 붕괴
        else
            dayFeeling = 0;

        talkCount = 0;
        orderCount = 0;
        base.Entered(enterEvt);
    }

    protected override void DecideNextBehaviour(AIBehaviour behaviour)
    {
        behaviour.feel = DecideFeel();
        behaviour.behaviour = DecideBehaviourType();
        behaviour.dialogue = DecideDialogue(behaviour);
    }

    private DialogueText DecideDialogue(AIBehaviour behaviour)
    {
        return dialogues.Query(
            behaviour.feel,
            behaviour.behaviour,
            information.reliance,
            drunk
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
        if (drunk >= 90)
            return BehaviourType.Exit;

        if (orderCount >= talkCount)
            return BehaviourType.Talk;
        else
            return BehaviourType.Order;
    }

    private CustomerFeel DecideFeel()
    {
        CustomerFeel feel = CustomerFeel.Find;
        if (information.visitCount == 1)
            return feel;

        float feelValue = information.reliance;
        feelValue += UnityEngine.Random.Range(-5f, 5);
        feelValue += dayFeeling;

        if (feelValue >= 65f)
            feel = CustomerFeel.Perfect;
        else if (feelValue >= 40f)
            feel = CustomerFeel.Good;
        else if (feelValue >= 0f)
            feel = CustomerFeel.Find;
        else if (feelValue >= -50f)
            feel = CustomerFeel.Bad;
        else
            feel = CustomerFeel.Terrible;

        return feel;
    }

    protected override int DecideVisit(int day)
    {
        int visitValue = 0;
        visitValue += (day - information.firstAppearDay) * 20;
        visitValue += 50;
        return visitValue;
    }

    public override void AddCocktail(CocktailDataSO cocktail)
    {
        drunk += 20;
        reaction = true;
    }
}
