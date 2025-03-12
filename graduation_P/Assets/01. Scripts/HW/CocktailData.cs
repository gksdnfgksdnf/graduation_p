using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCocktail", menuName = "SO/Cocktail/CocktailData")]
public class CocktailData : ScriptableObject
{
    public string cocktailName;
    public List<Ingredient> ingredients;
    public List<MakingTool> makingTool;   // How to mixing (Shake, Stir)

    [Header("Glass")]
    public Glass glassType;
}
