using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCocktail", menuName = "SO/Cocktail/CocktailData")]
public class CocktailDataSO : ScriptableObject
{
    public string cocktailName;
    public List<ItemSO> ingredients;
    public List<ItemSO> tools;
    public ItemSO glass;
}
