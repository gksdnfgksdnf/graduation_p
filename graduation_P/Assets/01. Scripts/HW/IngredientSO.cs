using UnityEngine;

public enum Ing_Type
{
    Alchole,
    Juice,
    Drink,
    Ganish,
    Ice,
    None
}

[CreateAssetMenu(fileName = "NewIng", menuName = "SO/Item/Ingredient")]
public class IngredientSO : ItemSO
{
    public Ing_Type type;

    [Range(0, 100)] public int alcoholContent;  // 도수
    [Range(0, 10)] public int bitterness;       // 쓴맛
    [Range(0, 10)] public int sweetness;        // 단맛
    [Range(0, 10)] public int sourness;         // 신맛
}
