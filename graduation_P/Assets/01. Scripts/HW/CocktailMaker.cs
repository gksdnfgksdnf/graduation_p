using System.Collections.Generic;
using UnityEngine;

public class CocktailMaker : MonoBehaviour
{
    public ItemListSO itemList; // 모든 아이템 목록
    public CocktailDataListSO cocktailDataList; // 칵테일 데이터 목록
    private void Start()
    {
        MakeCocktail("Negroni");
    }
    public void MakeCocktail(string cocktailName)
    {
        // 해당 칵테일 데이터 가져오기
        CocktailDataSO cocktailData = cocktailDataList.datas.Find(c => c.cocktailName == cocktailName);

        if (cocktailData == null)
        {
            Debug.LogError("칵테일을 찾을 수 없습니다.");
            return;
        }

        // 재료 준비
        List<ItemSO> ingredients = cocktailData.ingredients;
        List<ItemSO> tools = cocktailData.tools;
        ItemSO glass = cocktailData.glass;

        // 재료 출력
        Debug.Log($"칵테일 '{cocktailName}' 만들기!");
        Debug.Log("재료:");
        foreach (var ingredient in ingredients)
        {
            Debug.Log($"- {ingredient.itemName}");
        }

        // 도구 출력
        Debug.Log("사용할 도구:");
        foreach (var tool in tools)
        {
            Debug.Log($"- {tool.itemName}");
        }

        // 유리잔 출력
        Debug.Log($"유리잔: {glass.itemName}");

        // 칵테일 만드는 과정 (구체적인 로직을 추가할 수 있음)
        MixCocktail(ingredients, tools, glass);
    }

    private void MixCocktail(List<ItemSO> ingredients, List<ItemSO> tools, ItemSO glass)
    {
        // 믹싱 과정 (간단한 예시로, 재료를 섞는다는 출력)
        Debug.Log("칵테일 믹싱 중...");
        // 여기에 믹싱이나 쉐이킹, 필터링 등 구체적인 과정 추가 가능
        Debug.Log("칵테일 완성!");
    }
}
