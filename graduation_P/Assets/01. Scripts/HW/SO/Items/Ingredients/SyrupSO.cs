using UnityEngine;

[CreateAssetMenu(fileName = "NewSyrup", menuName = "SO/Item/Drink/Syrup")]
public class SyrupSO : IngredientSO
{
    [Range(0, 10)] public int sweetness;        // 단맛
    [Range(0, 10)] public int sourness;         // 신맛


}
