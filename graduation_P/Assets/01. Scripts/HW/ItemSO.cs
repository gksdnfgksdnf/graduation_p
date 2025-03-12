using UnityEngine;

public enum Item_Type
{
    Ingredient,
    MakingTool,
    Glass
}

public class ItemSO : ScriptableObject
{
    public string itemName;
    public Item_Type itemType;
}
