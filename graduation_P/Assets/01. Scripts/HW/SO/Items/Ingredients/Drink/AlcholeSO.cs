using UnityEngine;

[CreateAssetMenu(fileName = "NewAlchole", menuName = "SO/Item/Drink/Alchole")]
public class AlcholeSO : IngredientSO
{
    [Range(0, 60)] public int alcoholContent;  // 도수
    [Range(0, 5)] public int bitterness;       // 쓴맛
    [Range(0, 5)] public int sweetness;        // 단맛
    [Range(0, 5)] public int sourness;         // 신맛
    [Range(0, 3)] public int saltiness;        // 짠맛
}
