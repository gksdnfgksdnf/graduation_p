using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCocktail", menuName = "SO/Cocktail/CocktailData")]
public class CocktailDataSO : ScriptableObject
{
    public string cocktailName;
    public List<IngredientSO> ingredients;
    public List<ToolSO> tools;
    public GlassSO glassType;
}
