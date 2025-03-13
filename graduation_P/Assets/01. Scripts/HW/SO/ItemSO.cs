using UnityEngine;

public enum Item_Type
{
    Ingredient,
    Tool,
    Glass
}

public class ItemSO : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName;
    public string itemDescription;
    public Item_Type itemType;
    //public ItemRarity rarity;
    public Sprite icon;


    public int price;
    public float weight;
    public float volume;
    public bool isStackable;
    public bool isConsumable;
}
