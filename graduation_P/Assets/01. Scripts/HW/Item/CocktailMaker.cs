using System;
using UnityEngine;

public class CocktailMaker : MonoBehaviour
{
    public void ProcessAddIngredient(Item target, Ingredient ingredient)
    {
        if (target is Tool tool)
        {
            AddIngredientToContainer<Tool>(tool, ingredient);
        }
        else if (target is Cocktail cocktail)
        {
            AddIngredientToContainer<Cocktail>(cocktail, ingredient);
        }
    }

    public void ProcessUseTool(Item target, Tool tool)
    {
        if (target is Tool targetTool)
        {
            ApplyToolToContainer<Tool>(targetTool, tool);
        }
        else if (target is Cocktail cocktail)
        {
            ApplyToolToContainer<Cocktail>(cocktail, tool);
        }
    }

    private void AddIngredientToContainer<T>(T target, Ingredient ingredient) where T : IIngredientContainer
    {
        target.AddIngredient(ingredient);
    }

    private void ApplyToolToContainer<T>(T target, Tool tool) where T : IIngredientContainer
    {
        target.UseTool(tool);
    }
}
