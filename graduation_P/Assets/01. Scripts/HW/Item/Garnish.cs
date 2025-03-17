using UnityEngine;

public class Garnish : DraggableItem
{
    private void Awake()
    {
        itemType = ItemType.Ingredient;
    }

    public override void Use()
    {

    }
}
