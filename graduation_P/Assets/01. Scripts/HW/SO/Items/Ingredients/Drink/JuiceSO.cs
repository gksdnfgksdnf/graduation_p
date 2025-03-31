using UnityEngine;


[CreateAssetMenu(fileName = "NewJuice", menuName = "SO/Item/Drink/Juice")]
public class JuiceSO : IngredientSO
{
    [Range(0, 5)] public int bitterness;       // 쓴맛
    [Range(0, 5)] public int sweetness;        // 단맛
    [Range(0, 5)] public int sourness;         // 신맛
}
