public class ParanoiaAI : CustomerAI
{
    protected bool enter = false;
    protected int drunk = 0;
    protected float drunkMultiplier = 2f;
    protected int talkCount = 0;
    protected int orderCount = 0;

    public override void Entered()
    {
        enter = true;
        drunk = 0;
        talkCount = 0;
        orderCount = 0;
        base.Entered();
    }

    protected override void DecideNextBehaviour(AIBehaviour behaviour)
    {
        behaviour.feel = DecideFeel();

        if (enter)
        {

        }
        else
        {

        }
    }

    private CustomerFeel DecideFeel()
    {
        CustomerFeel feel = CustomerFeel.Find;
        if (information.visitCount == 1)
            return feel;

        float feelValue = information.reliance;

        // 망상증 변수
        feelValue += UnityEngine.Random.Range(-0.5f, 0.5f);

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

    protected override void DecideVisit(int day)
    {

    }
}
