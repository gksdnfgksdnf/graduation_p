using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CocktailMaker : MonoBehaviour
{
    public CocktailDataListSO cocktailDatas;  // 모든 칵테일 SO
    public List<IngredientSO> selectedIngs = new List<IngredientSO>();
    public List<ToolSO> selectedTools = new List<ToolSO>();

    public void MakeCocktail()
    {
        // 칵테일 검증
        CocktailDataSO result = ValidateCocktail();
        if (result != null)
        {
            Debug.Log(result.cocktailName + " 완성!");
            // UI 표시 및 효과 연출
        }
        else
        {
            Debug.Log("알고리즘을 이용해 비슷한 칵테일에 수식어를 붙여야 합니다.");
        }
        selectedIngs.Clear();
        selectedTools.Clear();
    }

    private CocktailDataSO ValidateCocktail()
    {
        foreach (var cocktail in cocktailDatas.datas)
        {
            // 1. 재료 개수
            if (cocktail.ingredients.Count != selectedIngs.Count) continue;


            // 2. 재료 목록 (순서 무관)
            var Ingredients = selectedIngs.OrderBy(i => i.itemName).ToList();
            var RecipeIng = cocktail.ingredients.OrderBy(i => i.itemName).ToList();

            if (!Ingredients.SequenceEqual(RecipeIng)) continue;


            // 3. 도구 목록 (순서 무관)
            var Tools = selectedTools.OrderBy(t => t.type.ToString()).ToList();
            var RecipeTools = cocktail.tools.OrderBy(t => t.type.ToString()).ToList();

            if (!Tools.SequenceEqual(RecipeTools)) continue;

            // 해당 칵테일 반환
            return cocktail;
        }

        // 모든 칵테일과 일치하지 않음
        return null;
    }
}
