using System.Collections.Generic;

public interface IIngredientContainer
{
    List<Item> Items { get; set; }
    void AddIngredient(Ingredient ingredient);
    void UseTool(Tool tool);
}
