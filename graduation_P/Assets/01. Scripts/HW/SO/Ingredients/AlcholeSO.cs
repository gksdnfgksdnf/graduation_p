using UnityEngine;

[CreateAssetMenu(fileName = "NewAlchole", menuName = "SO/Item/Drink/Alchole")]
public class AlcholeSO : ItemSO
{
    [Range(0, 100)] public int alcoholContent;  // 도수
    [Range(0, 10)] public int bitterness;       // 쓴맛
}
