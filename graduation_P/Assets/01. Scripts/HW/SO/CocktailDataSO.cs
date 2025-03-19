using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCocktail", menuName = "SO/Cocktail/CocktailData")]
public class CocktailDataSO : ItemSO
{
    public string cocktailName;
    public List<AlcholeSO> ingredients;
    public List<JuiceSO> juices;
    public List<SodaSO> sodas;
    public List<ItemSO> tools;
    public List<ItemSO> garnishes;
    public ItemSO glass;
}
