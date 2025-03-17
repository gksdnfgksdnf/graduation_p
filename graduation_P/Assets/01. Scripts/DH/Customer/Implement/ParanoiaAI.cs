using Random = UnityEngine.Random;

public class ParanoiaAI : CustomerAI
{
    protected bool enter = false;
    protected bool reaction = false;

    protected float dayFeeling = 0;

    protected int talkCount = 0;
    protected int orderCount = 0;

    public override void Entered(EnterEventType enterEvt)
    {
        enter = true;

        if (enterEvt == EnterEventType.Drunk)
            information.drunk = Random.Range(40, 60);
        else
            information.drunk = 0;

        if (enterEvt == EnterEventType.TerribleDay)
            dayFeeling = -33f;
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

    private DialogueHeader DecideDialogue(AIBehaviour behaviour)
    {
        return dialogues.Query(
            behaviour.feel,
            behaviour.behaviour,
            information.drunk,
            information.reliance
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
        else if (feelValue >= -40f)
            feel = CustomerFeel.Bad;
        else
            feel = CustomerFeel.Terrible;

        return feel;
    }

    public override bool DecideVisit(int day)
    {
        int visitValue = 0;
        visitValue += (day - information.firstAppearDay) * 50;
        return visitValue >= Random.Range(information.reliance, 100f);
    }

    public override void AddCocktail(CocktailDataSO cocktail)
    {
        information.drunk += 20;
        reaction = true;
    }
}
