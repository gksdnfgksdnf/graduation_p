using System.Collections.Generic;
using UnityEngine;

public class Cocktail : Item, IIngredientContainer
{
    [SerializeField]
    private List<Ingredient> ingredients = new List<Ingredient>();

    public List<Item> Items { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake()
    {
        Initialize(ItemType.Cocktail);
    }
    public List<Ingredient> GetIngredients()
    {
        return ingredients;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
        Debug.Log("칵테일에 재료 추가");
    }

    public void UseTool(Tool tool)
    {
        Debug.Log("칵테일에 도구 사용");
    }
}
