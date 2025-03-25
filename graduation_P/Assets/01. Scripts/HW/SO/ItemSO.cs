using UnityEngine;

public enum ItemType
{
    Cocktail,
    Ingredient,
    Tool,
    Glass,
    None,
}

public class ItemSO : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName;
    public Sprite icon;
    public bool isConsumable;

}
