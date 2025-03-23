using UnityEngine;

public class Garnish : Ingredient
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
