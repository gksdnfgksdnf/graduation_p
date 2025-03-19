public class MisterAI : CustomerAI
{
    public override bool DecideVisit(int day)
    {
        if (day == 1 || day % 6 == 0)
            return true;
        return false;
    }
}
