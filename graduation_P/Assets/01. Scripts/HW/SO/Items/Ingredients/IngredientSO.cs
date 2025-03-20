using UnityEngine;

public enum IngredientType
{
    None,
    Alchole,
    Syrup,
    Garnish,
    Soda,
    Juice,
    Sauce,
    Ice
}

public class IngredientSO : ItemSO
{
    public IngredientType ingType;

}
