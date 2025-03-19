using System.Linq;
using Random = UnityEngine.Random;

public class MisterAI : CustomerAI
{
    public override bool DecideVisit(int day)
    {
        if (day == 1)
            return true;
        return Random.Range(0, 100) > 15;
    }
}
