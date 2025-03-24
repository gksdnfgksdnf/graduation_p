using UnityEngine;

public class Garnish : Ingredient
{
    GanishSO garnishSO;
    protected override void Awake()
    {
        base.Awake();

        garnishSO = itemData as GanishSO;
    }

    public override void Use()
    {

    }
}
