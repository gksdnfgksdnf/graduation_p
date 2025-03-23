using UnityEngine;

public class Soda : Ingredient
{
    private SodaSO soda;

    private void Awake()
    {
        soda = itemData as SodaSO;
    }

    public override void Use()
    {
        
    }


}
