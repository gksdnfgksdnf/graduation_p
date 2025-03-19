using UnityEngine;

public enum IngredientType
{
    None,
    Alchole,
    Syrup,
    Garnish,
    Soda,
    Juice
}

public class IngredientSO : ItemSO
{
    public IngredientType ingType;

}
