using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Customer/Taste")]
public class CustomerTaste : ScriptableObject
{
    public List<IngredientSO> preferIngredients;
    public List<string> preferCocktails;
}
