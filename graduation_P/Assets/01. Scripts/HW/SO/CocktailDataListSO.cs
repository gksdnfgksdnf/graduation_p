using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCocktailList", menuName = "SO/Cocktail/CocktailList")]
public class CocktailDataListSO : ScriptableObject
{
    public List<CocktailDataSO> datas;
}
