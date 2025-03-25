public class DonCustomer : Customer
{
    public override bool Visit(int day, DayPhase phase)
    {
        return true;
    }
}
