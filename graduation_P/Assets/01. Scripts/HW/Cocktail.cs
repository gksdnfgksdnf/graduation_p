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

    public string GetName()
    {
        // 단순히 재료 이름을 합쳐서 반환
        string name = "칵테일: ";
        foreach (var ingredient in ingredients)
        {
            name += ingredient.name + " ";
        }
        return name.Trim();
    }
}
