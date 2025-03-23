using UnityEngine;

public class Garnish : Item
{
    GanishSO garnishSO;
    private void Awake()
    {
        garnishSO = itemData as GanishSO;
    }

    public override void Use()
    {

    }
}
