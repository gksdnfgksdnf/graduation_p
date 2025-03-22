using UnityEngine;

[CreateAssetMenu(fileName = "NewGanish", menuName = "SO/Item/Ganish")]

public class GanishSO : IngredientSO
{
    [Range(0, 10)] public int sweetness;        // 단맛
    [Range(0, 10)] public int sourness;         // 신맛
    [Range(0, 10)] public int bitness;         // 쓴맛
}
