using System;
using System.Collections.Generic;
using UnityEngine;

public class CocktailMaker : MonoBehaviour
{
    private Cocktail currentCocktail;

    public void StartNewCocktail()
    {
        currentCocktail = new Cocktail();
    }

    public void AddIngredient(Ingredient ingredient)
    {
        if (currentCocktail == null)
        {
            StartNewCocktail();
        }

        currentCocktail.AddIngredient(ingredient);

        // 칵테일 상태를 체크하여 완성 여부 확인
        if (IsCocktailComplete())
        {
            CompleteCocktail();
        }
    }

    private bool IsCocktailComplete()
    {
        // 예시: 재료 개수가 3개 이상이면 완성
        return currentCocktail.GetIngredients().Count >= 3;
    }

    private void CompleteCocktail()
    {
        Debug.Log("칵테일 완성: " + currentCocktail);
    }

    public void UseTool(Tool tool)
    {
        //이제 뭔가를 담고있는애인지, 다른곳에 사용되는 애인지를 구분해야함.
    }
}
