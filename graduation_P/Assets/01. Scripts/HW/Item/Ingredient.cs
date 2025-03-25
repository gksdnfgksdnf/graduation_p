using UnityEngine;

public class Ingredient : Item
{
    protected virtual void Awake()
    {
        itemType = ItemType.Ingredient;
    }
}
