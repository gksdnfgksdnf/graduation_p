using UnityEngine;

[CreateAssetMenu(fileName = "NewSoda", menuName = "SO/Item/Drink/Soda")]
public class SodaSO : ItemSO
{
    [Range(0, 10)] public int sweetness;        // 단맛
    [Range(0, 10)] public int sourness;         // 신맛
}
