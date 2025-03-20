using System.Collections.Generic;

public class Cocktail
{
    private CocktailDataSO cocktailData;
    private List<Ingredient> ingredients = new List<Ingredient>();
    private Glass glass;

    public void SetGlass(Glass glass)
    {
        this.glass = glass;
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
