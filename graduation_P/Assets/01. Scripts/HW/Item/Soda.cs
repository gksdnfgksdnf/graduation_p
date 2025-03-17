using UnityEngine;

public class Soda : DraggableItem
{
    private SodaSO soda;

    private void Awake()
    {
        soda = item as SodaSO;
        itemType = ItemType.Ingredient;
    }
    public override void Use()
    {
        
    }


}
