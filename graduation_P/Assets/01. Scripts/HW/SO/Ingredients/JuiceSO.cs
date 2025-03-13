using UnityEngine;


[CreateAssetMenu(fileName = "NewJuice", menuName = "SO/Item/Drink/Juice")]
public class JuiceSO : ItemSO
{
    [Range(0, 10)] public int sweetness;        // 단맛
    [Range(0, 10)] public int sourness;         // 신맛
}
