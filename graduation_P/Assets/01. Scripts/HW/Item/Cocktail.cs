using System.Collections.Generic;

public class Cocktail : Item
{
    private List<Ingredient> ingredients = new List<Ingredient>();
    private void Awake()
    {
        Initialize(ItemType.Cocktail);
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
    }

    public List<Ingredient> GetIngredients()
    {
        return ingredients;
    }
}
