using System;
using System.Collections.Generic;
using UnityEngine;

public class CocktailMaker : MonoBehaviour
{
    private Cocktail currentCocktail;


    public void AddIngredient(Ingredient ingredient)
    {
        currentCocktail ??= new Cocktail();

        currentCocktail.AddIngredient(ingredient);
    }

    public void UseTool(Tool tool)
    {
        //이제 뭔가를 담고있는애인지, 다른곳에 사용되는 애인지를 구분해야함.
    }

    public void UseGlass(Glass glass)
    {
        
    }
}
