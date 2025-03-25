public class NadyCustomer : Customer
{
    public override bool Visit(int day, DayPhase phase)
    {
        if (phase == DayPhase.Midnight)
        {
            return true;
        }
        return false;
    }
}
