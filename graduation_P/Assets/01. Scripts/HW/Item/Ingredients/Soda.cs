using UnityEngine;

public class Soda : Ingredient
{
    private SodaSO soda;

    protected override void Awake()
    {
        base.Awake();
        soda = itemData as SodaSO;
    }

    public override void Use()
    {
        
    }


}
