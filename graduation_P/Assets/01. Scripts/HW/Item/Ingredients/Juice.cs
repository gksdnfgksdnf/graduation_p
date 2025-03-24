using UnityEngine;

public class Juice : Ingredient
{
    private JuiceSO juice;

    protected override void Awake()
    {
        base.Awake();
        juice = itemData as JuiceSO;
    }

    public override void Use()
    {
        
    }

}
