using UnityEngine;

public class Soda : Item
{
    private SodaSO soda;

    private void Awake()
    {
        soda = item as SodaSO;
        itemType = Item_Type.Ingredient;
    }
    public override void Use()
    {
        
    }


}
