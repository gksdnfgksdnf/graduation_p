using UnityEngine;

public class Garnish : DraggableItem
{
    private void Awake()
    {
        itemType = Item_Type.Ingredient;
    }

    public override void Use()
    {

    }
}
