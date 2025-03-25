using System;
using System.Collections;
using UnityEngine;

public class CocktailMaker : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;

    private Vector3 originPosition;
    private float moveDuration = .2f; // 이동 시간
    private float threshold = 0.01f; // 도착 판단 거리

    private void Start()
    {
        originPosition = obj.transform.position;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OrderMenu();
        }
    }
    public void OrderMenu()
    {
        StartCoroutine(MoveToPosition(new Vector3(0, 0, 0)));
    }
    public void FinishMakeCocktail()
    {
        StartCoroutine(MoveToPosition(originPosition, 2));
    }
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

    private IEnumerator MoveToPosition(Vector3 target, float t = 0)
    {
        yield return new WaitForSeconds(t);

        while (Vector3.Distance(obj.transform.position, target) > threshold)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, target, Time.deltaTime * (1f / moveDuration));
            yield return null;
        }

        obj.transform.position = target; // 정확한 위치 보정
    }

}
