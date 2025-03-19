using UnityEngine;

public enum ItemType
{
    Cocktail,
    Ingredient,
    Tool,
    Glass
}

public class ItemSO : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName;
    public string itemDescription;
    public ItemType itemType;
    //public ItemRarity rarity;
    public Sprite icon;


    public int price;
    public float weight;
    public float volume;
    public bool isStackable;
    public bool isConsumable;
}
